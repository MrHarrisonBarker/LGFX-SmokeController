using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.Settings;

namespace LGFX_SmokeController.App.Main;

public partial class MainMenu : UserControl
{
    public MainMenu()
    {
        InitializeComponent();
    }

    private void OnSettingsClick( object sender, RoutedEventArgs e )
    {
        var window = new SettingsWindow();
        window.Show();
    }

    private void OnSmokeSettingsClick( object sender, RoutedEventArgs e )
    {
        var window = new SmokeSettingsWindow();
        window.Show();
    }
}