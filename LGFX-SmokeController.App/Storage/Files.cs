using System.IO;
using System.Xml.Serialization;

namespace LGFX_SmokeController.App.Storage;

public static class Files
{
    private static string Directory => Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "LGFXSmokeControl/" );
    public static readonly string SmokeMachines = Path.Combine( Files.Directory, "smoke_machines.xml" );
    public static readonly string Network = Path.Combine( Files.Directory, "network.xml" );


    public static void Save<T>( T obj, string path )
    {
        using var writer = new StreamWriter( path, new FileStreamOptions { Mode = FileMode.Create, Access = FileAccess.ReadWrite } );

        var xml = new XmlSerializer( typeof( T ) );
        xml.Serialize( writer, obj );

        writer.Close();
    }

    public static T? Load<T>( string path ) where T : class
    {
        try
        {
            using var reader = new StreamReader( path );
            var xml = new XmlSerializer( typeof( T ) );
            return xml.Deserialize( reader ) as T;
        }
        catch ( Exception e )
        {
            Console.WriteLine( e );
            return null;
        }
    }
}