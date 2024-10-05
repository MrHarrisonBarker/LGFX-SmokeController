using System.Collections.ObjectModel;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using LGFX_SmokeController.App.ArtNet;
using LGFX_SmokeController.App.Smoke;
using LGFX_SmokeController.App.Storage;

namespace LGFX_SmokeController.App;

public class Controller : ObservableObject
{
    private SmokeMachine? _SelectedMachine = null;

    public readonly ArtNetService ArtNetService;

    public SmokeMachine? SelectedMachine
    {
        get => _SelectedMachine;
        set => SetProperty( ref _SelectedMachine, value );
    }

    public ObservableCollection<SmokeMachine> SmokeMachines { get; set; } = [ ];
    private StorageManager StorageManager { get; }

    public Controller( Dispatcher dispatcher )
    {
        ArtNetService = new ArtNetService( dispatcher, this );
        StorageManager = new StorageManager( this );
    }

    public void Start() => ArtNetService.Start();

    public void Save()
    {
        StorageManager.Save();
    }

    public void New()
    {
        foreach ( var machine in SmokeMachines.ToList() )
        {
            SmokeMachines.Remove( machine );
        }

        StorageManager.Save();
    }

    public void ClearMachines()
    {
        foreach ( var machine in SmokeMachines.ToList() )
        {
            SmokeMachines.Remove( machine );
        }
    }

    public void SetMachines( params SmokeMachine[] smokeMachines )
    {
        foreach ( var smokeMachine in smokeMachines )
        {
            SmokeMachines.Add( smokeMachine );
        }
    }
}