using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Threading;
using ART.NET;
using CommunityToolkit.Mvvm.ComponentModel;
using Haukcode.HighResolutionTimer;

namespace LGFX_SmokeController.App.ArtNet;

public class ArtNetService : ObservableObject
{
    private readonly Controller Controller;
    private bool _IsBroadcasting;
    private short _Universe;
    public ART.NET.NetworkInterface? Adapter { get; set; }

    public short Universe
    {
        get => _Universe;
        set => SetProperty( ref _Universe, value );
    }

    public IEnumerable<ART.NET.NetworkInterface> AvailableAdapters
    {
        get
        {
            var interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            var supportedInterfaces = interfaces
                .Where( networkInterface => networkInterface is
                    { SupportsMulticast: true, OperationalStatus: OperationalStatus.Up } );

            foreach ( var supportedInterface in supportedInterfaces )
            {
                foreach ( var uniCastAddress in supportedInterface.GetIPProperties()
                             .UnicastAddresses )
                {
                    if ( uniCastAddress.Address.AddressFamily == AddressFamily.InterNetwork )
                        yield return new ART.NET.NetworkInterface(
                            supportedInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback
                                ? "Local host"
                                : supportedInterface.Description, uniCastAddress.Address, uniCastAddress.IPv4Mask );
                }
            }
        }
    }

    public ArtNetNodeManager? NodeManager { get; }
    public ObservableCollection<ArtNetNode> CustomNodes { get; set; } = [ ];

    public bool IsBroadcasting
    {
        get => _IsBroadcasting;
        set => SetProperty( ref _IsBroadcasting, value );
    }

    private ArtNetSocket? Socket { get; set; }
    private readonly Thread ArtNetThread;
    private const int ArtNetFrequency = 250;
    private CancellationTokenSource TokenSource = new ();


    public ArtNetService( Dispatcher dispatcher, Controller controller )
    {
        Controller = controller;
        Adapter = AvailableAdapters.FirstOrDefault();

        if ( Adapter is not null )
        {
            Socket = new ArtNetSocket( Adapter );
            NodeManager = new ArtNetNodeManager( Socket, "LGFX", "LGFX Smoke Controller", dispatcher );
        }

        ArtNetThread = new Thread( SendArtNet )
        {
            Name = "ArtNet Send Thread"
        };
    }

    private readonly HighResolutionTimer Timer = new ();

    private readonly ArtNetDmxBuffer Buffer = new ();
    private byte[] DmxBuffer = new byte[ 512 ];

    public void Start()
    {
        NodeManager?.StartPollReply();
        ArtNetThread.Start();
    }

    private void SendArtNet()
    {
        Timer.SetPeriod( ArtNetFrequency );
        Timer.Start();

        byte sequence = 0;

        while ( !TokenSource.IsCancellationRequested )
        {
            Timer.WaitForTrigger();

            Console.WriteLine( "Updating DMX buffer" );

            for ( var i = 0; i < 512; i++ )
            {
                DmxBuffer[ i ] = byte.MinValue;
            }

            for ( var c = 0; c < Controller.SmokeMachines.Count; c++ )
            {
                if ( Controller.SmokeMachines[ c ].IsThreeChannel )
                {
                    DmxBuffer[ Controller.SmokeMachines[ c ].HeatAddress - 1 ] = 255;
                }

                DmxBuffer[ Controller.SmokeMachines[ c ].Address - 1 ] = Controller.SmokeMachines[ c ].SmokeValue();
                DmxBuffer[ Controller.SmokeMachines[ c ].FanAddress - 1 ] = Controller.SmokeMachines[ c ].FanValue();
            }

            Console.WriteLine( "Sending ArtNet data" );

            Buffer.SetSequence( sequence );
            Buffer.SetUniverse( Universe );
            Buffer.SetData( DmxBuffer );

            if ( IsBroadcasting )
            {
                Socket?.Send( Buffer );
            }
            else
            {
                if ( NodeManager is not null )
                {
                    for ( var n = 0; n < NodeManager.Nodes.Count; n++ )
                    {
                        if ( NodeManager.Nodes[ n ].IsSending )
                        {
                            Socket?.Send( Buffer, NodeManager.Nodes[ n ].Address );
                        }
                    }

                    for ( var n = 0; n < CustomNodes.Count; n++ )
                    {
                        if ( CustomNodes[ n ].IsSending )
                        {
                            Socket?.Send( Buffer, CustomNodes[ n ].Address );
                        }
                    }
                }
            }
        }
    }
}