using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using ART.NET;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LGFX_SmokeController.App.ArtNet;

public class ArtNetService : ObservableObject
{
    private bool _IsBroadcasting;
    public ART.NET.NetworkInterface? Adapter { get; set; }

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

    public ArtNetSocket? Socket { get; set; }

    // public ObservableCollection<ArtNetNode> NodesSendingTo { get; set; } = [ ];

    public ArtNetService( Dispatcher dispatcher )
    {
        Adapter = AvailableAdapters.FirstOrDefault();

        if ( Adapter is not null )
        {
            Socket = new ArtNetSocket( Adapter );
            NodeManager = new ArtNetNodeManager( Socket, "LGFX", "LGFX Smoke Controller", dispatcher );
            // NodeManager.Nodes.CollectionChanged += ( _, _ ) =>
            // {
            //     Console.WriteLine("NODES CHANGED");
            //     OnPropertyChanged(nameof(NodesSendingTo));
            // };
        }
        
        
    }
}