using System.Net;
using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.ArtNet;

namespace LGFX_SmokeController.App.Settings;

public partial class SettingsWindow : Window
{
    public int[] UniverseDefaults { get; } = [ 0, 1, 2, 3, 4, 5 ];
    public ArtNetService ArtNetService => ( ( App )Application.Current ).Controller.ArtNetService;

    public SettingsWindow()
    {
        InitializeComponent();
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        Close();
    }

    private void OnRefreshNodesClick( object sender, RoutedEventArgs e )
    {
        ArtNetService.NodeManager?.Refresh();
    }

    private void OnAddCustomNodeClick( object sender, RoutedEventArgs e )
    {
        ArtNetService.CustomNodes.Add( new ArtNetNode( "", "", new IPAddress( CustomNodeIp.IpAddressBytes ), true ) );
    }

    private void OnRemoveCustomNodeClick( object sender, RoutedEventArgs e )
    {
        if ( ( ( Button )sender ).Tag is ArtNetNode node )
        {
            ArtNetService.CustomNodes.Remove( node );
        }
    }

    private void OnRemoveNodeClick( object sender, RoutedEventArgs e )
    {
        if ( ( ( Button )sender ).Tag is ArtNetNode node )
        {
            ArtNetService.NodeManager?.Nodes.Remove( node );
        }
    }
}