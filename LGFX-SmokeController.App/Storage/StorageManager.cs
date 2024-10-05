using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using ART.NET;
using LGFX_SmokeController.App.ArtNet;
using LGFX_SmokeController.App.Smoke;
using LGFX_SmokeController.App.Storage.Network;
using LGFX_SmokeController.App.Storage.SmokeMachine;

namespace LGFX_SmokeController.App.Storage;

public class StorageManager
{
    private readonly Controller Controller;

    public StorageManager( Controller controller )
    {
        Controller = controller;
        Load();
    }

    public void Save()
    {
        if ( !Directory.Exists( Files.Directory )  ) Directory.CreateDirectory( Files.Directory );

        new StoredSmokeMachines( Controller.SmokeMachines.Select( m => new StoredSmokeMachine( m ) ).ToList() ).Save();
        new StoredNetwork( Controller.ArtNetService ).Save();
    }

    public void SaveToFile( string path )
    {
    }

    public void Load()
    {
        var storedSmokeMachines = StoredSmokeMachines.Load();

        if ( storedSmokeMachines is not null )
        {
            Controller.SetMachines( storedSmokeMachines.SmokeMachines.Select( StoredSmokeMachine.SmokeMachine ).ToArray() );
        } else
        {
            Controller.SetMachines(
                new Smoke.SmokeMachine("SL 1", 1, SmokeMachinePresets.Hazer),
                new Smoke.SmokeMachine("SL 2", 3, SmokeMachinePresets.Hazer),
                new Smoke.SmokeMachine("SR 3", 5, SmokeMachinePresets.Hazer),
                new Smoke.SmokeMachine("SR 4", 7, SmokeMachinePresets.Hazer),
                new Smoke.SmokeMachine("FOH 4", 9, SmokeMachinePresets.Hazer),
                new Smoke.SmokeMachine("FOH 5", 11, SmokeMachinePresets.Hazer),
                new Smoke.SmokeMachine("DELAY 6", 13, SmokeMachinePresets.Hazer),
                new Smoke.SmokeMachine("DELAY 7", 15, SmokeMachinePresets.Hazer)
            );
        }

        var storedNetwork = StoredNetwork.Load();

        if ( storedNetwork is not null )
        {
            if ( storedNetwork.Adapter is not null )
            {
                var networkInterface = new NetworkInterface(
                    storedNetwork.Adapter.Name,
                    new IPAddress( storedNetwork.Adapter.Address ),
                    new IPAddress( storedNetwork.Adapter.Subnet )
                );

                if ( NetworkInterface.AvailableInterfaces.Contains( networkInterface ) )
                {
                    Controller.ArtNetService.Adapter = networkInterface;
                }
            }

            Controller.ArtNetService.CustomNodes = new ObservableCollection<ArtNetNode>( storedNetwork.CustomNodes.Select( node => new ArtNetNode( node.ShortName, node.LongName, new IPAddress( node.Address ), node.IsSending ) ) );
            Controller.ArtNetService.Nodes = new ObservableCollection<ArtNetNode>( storedNetwork.ConnectedNodes.Select( node => new ArtNetNode( node.ShortName, node.LongName, new IPAddress( node.Address ), node.IsSending, false ) ) );

            Controller.ArtNetService.IsBroadcasting = storedNetwork.IsBroadcasting;
            Controller.ArtNetService.Universe = storedNetwork.Universe;
        }
    }

    public void LoadFromFile( string path )
    {
    }
}