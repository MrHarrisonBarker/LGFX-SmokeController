using ART.NET;

namespace LGFX_SmokeController.App.Storage.Network;

public class StoredNetworkInterface
{
    public string Name { get; set; }
    public byte[] Address { get; set; }
    public byte[] Subnet { get; set; }

    public StoredNetworkInterface( NetworkInterface networkInterface )
    {
        Name = networkInterface.Name;
        Address = networkInterface.Address.GetAddressBytes();
        Subnet = networkInterface.Subnet.GetAddressBytes();
    }

    public StoredNetworkInterface()
    {
    }
}