using System.Windows;
using LGFX_SmokeController.App.ArtNet;

namespace LGFX_SmokeController.App;

public partial class App : Application
{
    public ArtNetService ArtNetService;

    public App()
    {
        ArtNetService = new ArtNetService( Dispatcher );
    }
}