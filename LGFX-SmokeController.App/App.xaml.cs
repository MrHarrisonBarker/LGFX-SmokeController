using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using LGFX_SmokeController.App.ArtNet;
using LGFX_SmokeController.App.Smoke;

namespace LGFX_SmokeController.App;

public class Controller : ObservableObject
{
    private SmokeMachine? _SelectedMachine = null;

    public SmokeMachine? SelectedMachine
    {
        get => _SelectedMachine;
        set => SetProperty( ref _SelectedMachine, value );
    }
}

public partial class App : Application
{
    public ArtNetService ArtNetService;

    public ObservableCollection<SmokeMachine> SmokeMachines { get; set; } =
    [
        new SmokeMachine( "SL 1", 1, SmokeMachinePresets.Hazer ),
        new SmokeMachine( "SL 2", 3, SmokeMachinePresets.Hazer ),
        new SmokeMachine( "SR 3", 5, SmokeMachinePresets.Hazer ),
        new SmokeMachine( "SR 4", 7, SmokeMachinePresets.Hazer ),
        new SmokeMachine( "FOH 4", 7, SmokeMachinePresets.Hazer ),
        new SmokeMachine( "FOH 5", 7, SmokeMachinePresets.Hazer ),
        new SmokeMachine( "DELAY 6", 7, SmokeMachinePresets.Hazer ),
        new SmokeMachine( "DELAY 7", 7, SmokeMachinePresets.Hazer ),
    ];

    public Controller Controller { get; } = new Controller();
    

    public App()
    {
        ArtNetService = new ArtNetService( Dispatcher );
    }
}