using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.Smoke;

namespace LGFX_SmokeController.App.Settings;

public partial class SmokeSettingsWindow : Window
{
    public App App => ( App )Application.Current;
    
    public int[] SmokeTimingDefaults { get; } = [ 10, 20, 40, 60 ];
    public byte[] AddressDefaults { get; } = [ 1, 3, 5, 7, 9, 11, 13, 15, 17 ];

    public SmokeSettingsWindow()
    {
        InitializeComponent();
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        Close();
    }

    private void OnAddClick( object sender, RoutedEventArgs e )
    {
        var window = new AddSmokeMachineWindow();
        window.Show();
    }

    private void OnMachineSelected( object sender, SelectionChangedEventArgs e )
    {
        
    }

    private void OnRemoveClick( object sender, RoutedEventArgs e )
    {
        if ( ListOfMachines.SelectedItem is SmokeMachine machine )
        {
            App.SmokeMachines.Remove( machine );
            ListOfMachines.SelectedItem = null;
        }
    }
}