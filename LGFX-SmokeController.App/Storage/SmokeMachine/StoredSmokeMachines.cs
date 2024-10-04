using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LGFX_SmokeController.App.Storage.SmokeMachine;

public class StoredSmokeMachines
{
    public StoredSmokeMachines( List<StoredSmokeMachine> smokeMachines )
    {
        SmokeMachines = smokeMachines;
    }

    public StoredSmokeMachines()
    {
    }


    public List<StoredSmokeMachine> SmokeMachines { get; set; } = [ ];

    public void Save()
    {
        Files.Save( this, Files.SmokeMachines );
    }

    public static StoredSmokeMachines? Load()
    {
        return Files.Load<StoredSmokeMachines>( Files.SmokeMachines );
    }
}