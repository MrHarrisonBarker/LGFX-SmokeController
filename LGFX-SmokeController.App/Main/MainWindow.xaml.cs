using System.Windows;

namespace LGFX_SmokeController.App.Main;

public partial class MainWindow : Window
{
    public App App => ( App )Application.Current;
        
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnStopAllClick( object sender, RoutedEventArgs e )
    {
        foreach ( var machine in App.SmokeMachines )
        {
            machine.Stop();
        }
    }

    private void OnTriggerAll100( object sender, RoutedEventArgs e )
    {
        
    }

    private void OnTriggerAll( object sender, RoutedEventArgs e )
    {
        foreach ( var machine in App.SmokeMachines )
        {
            machine.Trigger();
        }
    }
}