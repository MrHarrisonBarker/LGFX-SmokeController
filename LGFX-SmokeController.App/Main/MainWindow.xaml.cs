using System.Windows;

namespace LGFX_SmokeController.App.Main;

public partial class MainWindow : Window
{
    public Controller Controller => ( ( App )Application.Current ).Controller;
        
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnStopAllClick( object sender, RoutedEventArgs e )
    {
        foreach ( var machine in Controller.SmokeMachines )
        {
            machine.Stop();
        }
    }

    private void OnTriggerAll100( object sender, RoutedEventArgs e )
    {
        foreach ( var machine in Controller.SmokeMachines )
        {
            machine.SmokeLevel = 255;
            machine.FanLevel = 255;
            machine.Trigger();
        }
    }

    private void OnTriggerAll( object sender, RoutedEventArgs e )
    {
        foreach ( var machine in Controller.SmokeMachines )
        {
            machine.Trigger();
        }
    }
}