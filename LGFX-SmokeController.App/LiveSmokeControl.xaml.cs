using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.Smoke;

namespace LGFX_SmokeController.App;

public partial class LiveSmokeControl : UserControl
{
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
    }
}