using System.Windows;

namespace LGFX_SmokeController.App.Settings;

public partial class SettingsWindow : Window
{
    public SettingsWindow()
    {
        InitializeComponent();
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        Close();
    }
}