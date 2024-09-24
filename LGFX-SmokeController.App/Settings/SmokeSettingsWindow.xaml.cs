using System.Windows;

namespace LGFX_SmokeController.App.Settings;

public partial class SmokeSettingsWindow : Window
{
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
}