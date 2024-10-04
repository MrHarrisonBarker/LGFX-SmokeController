using System.IO;
using System.Text;
using System.Xml.Serialization;
using LGFX_SmokeController.App.ArtNet;

namespace LGFX_SmokeController.App.Storage.Network;

public class StoredNetwork
{
    public bool IsBroadcasting { get; set; } = false;
    public short Universe { get; set; } = 0;
    public List<StoredArtNetNode> CustomNodes { get; set; } = [ ];
    public List<StoredArtNetNode> ConnectedNodes { get; set; } = [ ];
    public StoredNetworkInterface? Adapter { get; set; } = null;

    public StoredNetwork( ArtNetService service )
    {
        IsBroadcasting = service.IsBroadcasting;
        Universe = service.Universe;
        CustomNodes = service.CustomNodes.Select( x => new StoredArtNetNode( x ) ).ToList();
        ConnectedNodes = service.NodeManager?.Nodes.Select( x => new StoredArtNetNode( x ) ).ToList() ?? [ ];
        Adapter = service.Adapter is not null ? new StoredNetworkInterface( service.Adapter ) : null;
    }

    public StoredNetwork()
    {
    }

    public void Save()
    {
        using var writer = new StreamWriter( Files.Network, Encoding.UTF8, new FileStreamOptions
        {
            Access = FileAccess.Write,
            Mode = FileMode.OpenOrCreate
        } );

        var xml = new XmlSerializer( typeof( StoredNetwork ) );
        xml.Serialize( writer, this );
    }

    public static StoredNetwork? Load()
    {
        using var reader = new StreamReader( Files.Network );
        var xml = new XmlSerializer( typeof( StoredNetwork ) );
        return xml.Deserialize( reader ) as StoredNetwork;
    }
}