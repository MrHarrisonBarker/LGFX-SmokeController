using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.Smoke.SmokeModes;

namespace LGFX_SmokeController.App.Smoke;

public partial class LiveSmokeControl : UserControl
{
    public Controller Controller => ( ( App )Application.Current ).Controller;
    public LiveSmokeControl()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty MachineProperty = DependencyProperty.Register( nameof( Machine ), typeof( SmokeMachine ), typeof( LiveSmokeControl ) );

    public SmokeMachine Machine
    {
        get => ( SmokeMachine )GetValue( MachineProperty );
        set => SetValue( MachineProperty, value );
    }

    private void OnTriggerClick( object sender, RoutedEventArgs e )
    {
        Machine.Toggle();
    }

    private void OnModeClick( object sender, RoutedEventArgs e )
    {
        Machine.StopImmediately();

        if ( Machine.SmokeMode is TimedMode )
        {
            Machine.SmokeMode = new TriggerMode( Machine );
        }
        else
        {
            Machine.SmokeMode = new TimedMode( Machine );
        }
    }
}