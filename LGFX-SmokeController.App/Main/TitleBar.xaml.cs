using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace LGFX_SmokeController.App.Main;

public partial class TitleBar : UserControl
{
    private Window Window => ( ( Window )( ( DockPanel )( ( Grid )Parent ).Parent ).Parent );
    private static Version AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version ?? new Version(0,0,0);
    public string Version => $"v{AssemblyVersion.Major}.{AssemblyVersion.Minor}.{AssemblyVersion.MinorRevision}";

    public TitleBar()
    {
        InitializeComponent();
    }

    private void OnMinimizeClick( object sender, RoutedEventArgs e )
    {
        Window.WindowState = WindowState.Minimized;
    }

    private void OnMaximizeClick( object sender, RoutedEventArgs e )
    {
        Window.WindowState = Window.WindowState is WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        ( ( App )Application.Current ).Controller.Save();
        Environment.Exit( 0 );
    }
}