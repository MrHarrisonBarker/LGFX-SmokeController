﻿using System.Net;
using System.Windows;
using System.Windows.Controls;
using ART.NET;
using LGFX_SmokeController.App.ArtNet;

namespace LGFX_SmokeController.App.Settings;

public partial class SettingsWindow : Window
{
    public int[] UniverseDefaults { get; } = [ 0, 1, 2, 3, 4, 5 ];

    private Controller Controller => ( Controller )DataContext;
    private ArtNetService ArtNetService => Controller.ArtNetService;

    public IEnumerable<NetworkInterface> Adapters => NetworkInterface.AvailableInterfaces;

    public SettingsWindow( Controller controller )
    {
        DataContext = controller;
        InitializeComponent();

        Closed += ( _, _ ) => Controller.Save();
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        Close();
    }

    private void OnRefreshNodesClick( object sender, RoutedEventArgs e )
    {
        ArtNetService.RefreshNodes();
    }

    private void OnAddCustomNodeClick( object sender, RoutedEventArgs e )
    {
        ArtNetService.CustomNodes.Add( new ArtNetNode( "", "", new IPAddress( CustomNodeIp.GetByteArray() ), true ) );
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
            ArtNetService.Nodes.Remove( node );
        }
    }
}