using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LGFX_SmokeController.App.ArtNet;

public class ArtNetNode : ObservableObject
{
    private bool _IsSending;
    private bool _IsConnected;

    public ArtNetNode( string shortName, string longName, IPAddress address, bool isSending = false, bool isConnected = true )
    {
        IsSending = isSending;
        ShortName = shortName;
        LongName = longName;
        Address = address;
        IsConnected = isConnected;
    }

    public bool IsSending
    {
        get => _IsSending;
        set => SetProperty( ref _IsSending, value );
    }

    public bool IsConnected
    {
        get => _IsConnected;
        set => SetProperty( ref _IsConnected, value );
    }

    public string ShortName { get; init; }
    public string LongName { get; init; }
    public IPAddress Address { get; init; }

    public override string ToString()
    {
        return $"{Address}:{LongName}, isSending: {IsSending}";
    }
}