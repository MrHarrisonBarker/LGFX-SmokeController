using System.Windows;
using LGFX_SmokeController.App.Smoke;

namespace LGFX_SmokeController.App.Main;

public partial class MainWindow : Window
{
    public Controller Controller => ( ( App )Application.Current ).Controller;
        
    public MainWindow()
    {
        
        
        InitializeComponent();
        
        Width = double.Max( ( Controller.SmokeMachines.Count * 120 ) + 120, 440 );
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
            machine.SmokeLevel = SmokeMachine.MaxLevel;
            machine.FanLevel = SmokeMachine.MaxLevel;
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