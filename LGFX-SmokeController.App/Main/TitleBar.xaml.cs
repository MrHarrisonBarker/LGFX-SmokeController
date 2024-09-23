using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace LGFX_SmokeController.App.Main;

public partial class TitleBar : UserControl
{
    public string Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "UNKNOWN";
    
    public TitleBar()
    {
        InitializeComponent();
    }
    
    private void OnMinimizeClick( object sender, RoutedEventArgs e )
    {
        // Window.WindowState = WindowState.Minimized;
    }

    private void OnMaximizeClick( object sender, RoutedEventArgs e )
    {
        // Window.WindowState = Window.WindowState is WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        // App.Controller.Mode = ControllerMode.Disabled;
        // var shutdownWindow = new ShutdownWindow();
        // shutdownWindow.ShowDialog();
    }
}