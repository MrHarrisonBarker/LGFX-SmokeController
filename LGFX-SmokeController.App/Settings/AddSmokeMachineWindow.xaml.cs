using System.Windows;

namespace LGFX_SmokeController.App.Settings;

public partial class AddSmokeMachineWindow : Window
{
    public byte[] AddressDefaults { get; } = [ 1, 3, 5, 7, 9, 11, 13, 15, 17 ];
    
    public AddSmokeMachineWindow()
    {
        InitializeComponent();
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        Close();
    }
}