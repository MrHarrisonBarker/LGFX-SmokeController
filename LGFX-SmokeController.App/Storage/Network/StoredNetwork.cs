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
        Files.Save( this, Files.Network );
    }

    public static StoredNetwork? Load()
    {
        return Files.Load<StoredNetwork>( Files.Network );
    }
}