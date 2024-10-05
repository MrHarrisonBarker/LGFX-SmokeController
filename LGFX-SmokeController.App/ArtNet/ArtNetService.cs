using System.Collections.ObjectModel;
using System.Windows.Threading;
using ART.NET;
using CommunityToolkit.Mvvm.ComponentModel;
using Haukcode.HighResolutionTimer;
using NetworkInterface = ART.NET.NetworkInterface;

namespace LGFX_SmokeController.App.ArtNet;

public class ArtNetService : ObservableObject
{
    private const int ArtNetFrequency = 250;

    private readonly Dispatcher? Dispatcher;
    private readonly Controller Controller;

    private readonly ArtNetDmxBuffer Buffer = new ();
    private readonly byte[] DmxBuffer = new byte[ 512 ];


    private readonly CancellationTokenSource TokenSource = new ();
    private CancellationToken Token => TokenSource.Token;
    private readonly HighResolutionTimer Timer = new ();


    private ArtNetSocket? Socket { get; set; }
    private readonly Thread ArtNetThread;


    private NetworkInterface? _Adapter;
    private bool _IsBroadcasting;
    private short _Universe;

    private ArtNetNodeManager? NodeManager { get; set; }
    public ObservableCollection<ArtNetNode> CustomNodes { get; set; } = [ ];
    public ObservableCollection<ArtNetNode> Nodes { get; set; } = [ ];

    public NetworkInterface? Adapter
    {
        get => _Adapter;
        set
        {
            if ( value == _Adapter ) return;
            SetProperty( ref _Adapter, value );
            CreateSocketForAdapter();
        }
    }

    public short Universe
    {
        get => _Universe;
        set => SetProperty( ref _Universe, value );
    }


    public bool IsBroadcasting
    {
        get => _IsBroadcasting;
        set => SetProperty( ref _IsBroadcasting, value );
    }


    public ArtNetService( Dispatcher? dispatcher, Controller controller )
    {
        Dispatcher = dispatcher;
        Controller = controller;
        Adapter = NetworkInterface.AvailableInterfaces.FirstOrDefault();

        ArtNetThread = new Thread( SendArtNet )
        {
            Name = "ArtNet Send Thread"
        };
    }

    private void CreateSocketForAdapter()
    {
        if ( Adapter is not null )
        {
            Console.WriteLine( "Creating new socket from adapter" );
            if ( NodeManager is not null ) NodeManager.PollReceived -= NodeManagerOnPollReceived;
            ClearNodes();
            NodeManager?.Stop();

            Socket?.Dispose();
            Socket?.Close();

            Socket = new ArtNetSocket( Adapter );
            NodeManager = new ArtNetNodeManager( Socket, "LGFX", "LGFX Smoke Controller" );
            NodeManager.StartPollReply();

            NodeManager.PollReceived += NodeManagerOnPollReceived;
        }
    }

    private void NodeManagerOnPollReceived( ArtNetNode node )
    {
        Console.WriteLine( $"Got node!, {node}" );
        var existing = Nodes.FirstOrDefault( x => Equals( x.Address, node.Address ) );

        if ( existing is null )
        {
            Console.WriteLine( Dispatcher );
            Dispatcher?.Invoke( () =>
            {
                Nodes.Add( new ArtNetNode( node.ShortName, node.LongName, node.Address ) );
                Console.WriteLine( $"Nodes -> {string.Join( ",", Nodes )}" );
            } );
        }
        else
        {
            existing.IsConnected = true;
        }
    }

    private void ClearNodes()
    {
        Console.WriteLine( "Clearing nodes" );
        foreach ( var node in Nodes.ToList() )
        {
            Nodes.Remove( node );
        }
    }

    public void Start()
    {
        Console.WriteLine( $"Socket {Socket}, {Adapter}s" );
        ArtNetThread.Start();
    }

    private void SendArtNet()
    {
        Timer.SetPeriod( ArtNetFrequency );
        Timer.Start();

        byte sequence = 0;

        while ( !Token.IsCancellationRequested )
        {
            Timer.WaitForTrigger();

            // Console.WriteLine( "Updating DMX buffer" );
            // Console.WriteLine( $"Socket {Socket?.NetworkInterface}" );

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

            // Console.WriteLine( "Sending ArtNet data" );

            Buffer.SetSequence( sequence++ );
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
                    for ( var n = 0; n < Nodes.Count; n++ )
                    {
                        if ( Nodes[ n ].IsSending )
                        {
                            Socket?.Send( Buffer, Nodes[ n ].Address );
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

    public void RefreshNodes()
    {
        ClearNodes();
        NodeManager?.Refresh();
    }
}