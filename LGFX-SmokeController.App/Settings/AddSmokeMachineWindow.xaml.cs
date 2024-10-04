using System.Windows;
using LGFX_SmokeController.App.Smoke;

namespace LGFX_SmokeController.App.Settings;

public partial class AddSmokeMachineWindow : Window
{
    public Controller Controller => ( ( App )Application.Current ).Controller;

    public byte[] AddressDefaults { get; } = [ 1, 3, 5, 7, 9, 11, 13, 15, 17 ];

    public SmokeMachinePresets MachinePreset { get; set; } = SmokeMachinePresets.Hazer;
    public int MachineAddress { get; set; }
    public string MachineName { get; set; } = "SL";


    public AddSmokeMachineWindow()
    {
        MachineAddress = Controller.SmokeMachines.LastOrDefault()?.Address + 2 ?? 1;
        
        InitializeComponent();
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        Close();
    }

    private void OnAddClick( object sender, RoutedEventArgs e )
    {
        Controller.SmokeMachines.Add( new SmokeMachine( MachineName, ( short )MachineAddress, MachinePreset ) );

        Close();
    }
}