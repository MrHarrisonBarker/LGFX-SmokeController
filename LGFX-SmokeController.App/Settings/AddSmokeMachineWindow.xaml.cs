using System.Windows;
using LGFX_SmokeController.App.Smoke;
using LGFX_SmokeController.App.Smoke.FanModes;

namespace LGFX_SmokeController.App.Settings;

public partial class AddSmokeMachineWindow : Window
{
    private Controller Controller => ( Controller )DataContext;

    public byte[] AddressDefaults { get; } = [ 1, 3, 5, 7, 9, 11, 13, 15, 17 ];

    public SmokeMachinePresets MachinePreset { get; set; } = SmokeMachinePresets.Hazer;
    public int MachineAddress { get; set; }
    public string MachineName { get; set; } = "SL";

    public string[] FanModes { get; } = [ "Instant", "Timed", "Constant" ];

    public string FanMode { get; set; } = "Timed";

    public AddSmokeMachineWindow( Controller controller )
    {
        DataContext = controller;
        MachineAddress = Controller.SmokeMachines.LastOrDefault()?.Address + 2 ?? 1;

        InitializeComponent();
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        Close();
    }

    private void OnAddClick( object sender, RoutedEventArgs e )
    {
        var smokeMachine = new SmokeMachine( MachineName, ( short )MachineAddress, MachinePreset );

        switch ( FanMode )
        {
            case "Instant":
                smokeMachine.FanMode = new InstantFanMode( smokeMachine );
                break;
            case "Timed":
                smokeMachine.FanMode = new TimedFanMode( smokeMachine );
                break;
            case "Constant":
                smokeMachine.FanMode = new ConstantFanMode( smokeMachine );
                break;
        }

        Controller.SmokeMachines.Add( smokeMachine );

        Close();
    }
}