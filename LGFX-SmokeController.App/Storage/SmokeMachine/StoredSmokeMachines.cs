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
        using var writer = new StreamWriter( Files.SmokeMachines, Encoding.UTF8, new FileStreamOptions
        {
            Access = FileAccess.Write,
            Mode = FileMode.OpenOrCreate
        } );
        
        var xml = new XmlSerializer( typeof( StoredSmokeMachines ) );
        xml.Serialize( writer, this );
    }

    public static StoredSmokeMachines? Load()
    {
        using var reader = new StreamReader( Files.SmokeMachines );
        var xml = new XmlSerializer( typeof( StoredSmokeMachines ) );
        return xml.Deserialize( reader ) as StoredSmokeMachines;
    }
}