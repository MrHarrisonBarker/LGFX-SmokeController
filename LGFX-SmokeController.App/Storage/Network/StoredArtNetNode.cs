using LGFX_SmokeController.App.ArtNet;

namespace LGFX_SmokeController.App.Storage.Network;

public class StoredArtNetNode
{
    public bool IsSending { get; set; }

    public string ShortName { get; set; }
    public string LongName { get; set; }
    public byte[] Address { get; set; }

    public StoredArtNetNode( ArtNetNode node )
    {
        IsSending = node.IsSending;
        ShortName = node.ShortName;
        LongName = node.LongName;
        Address = node.Address.GetAddressBytes();
    }

    public StoredArtNetNode()
    {
    }
}