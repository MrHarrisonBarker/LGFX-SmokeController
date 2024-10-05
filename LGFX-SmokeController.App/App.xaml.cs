using System.Diagnostics;
using System.Windows;

namespace LGFX_SmokeController.App;

public partial class App : Application
{
    public Controller Controller { get; }


    public App()
    {
        Controller = new Controller( Dispatcher );
    }

    protected override void OnStartup( StartupEventArgs e )
    {
        var proc = Process.GetCurrentProcess();
        var count = Process.GetProcesses().Count( p => p.ProcessName == proc.ProcessName );

        if (count > 1)
        {
            MessageBox.Show("LGFX Smoke Controller is already running...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Current.Shutdown();
        }
        
        base.OnStartup( e );
        
        Controller.Start();
    }
}