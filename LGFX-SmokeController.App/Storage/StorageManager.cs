using LGFX_SmokeController.App.Storage.Network;
using LGFX_SmokeController.App.Storage.SmokeMachine;

namespace LGFX_SmokeController.App.Storage;

public class StorageManager
{
    private readonly Controller Controller;

    public StorageManager( Controller controller )
    {
        Controller = controller;
    }

    public void Save()
    {
        new StoredSmokeMachines( Controller.SmokeMachines.Select(m => new StoredSmokeMachine(m)).ToList() ).Save();
        new StoredNetwork( Controller.ArtNetService ).Save();
    }
}