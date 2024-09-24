using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.Settings;
using Microsoft.Win32;

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

    private void OnDefaultPresetClick( object sender, RoutedEventArgs e )
    {
        
    }

    private void OnSavePresetClick( object sender, RoutedEventArgs e )
    {
        
    }

    private void OnOpenPresetClick( object sender, RoutedEventArgs e )
    {
        var dialog = new OpenFileDialog
        {
            Multiselect = false,
            Title = "Open Preset",
            CheckFileExists = true,
            Filter = LgfxSmokeFileExtension.Filter,
            DefaultExt = LgfxSmokeFileExtension.Extension
        };

        dialog.ShowDialog();
    }
}