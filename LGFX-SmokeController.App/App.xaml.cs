using System.Windows;

namespace LGFX_SmokeController.App;

public partial class App : Application
{
    public Controller Controller { get; }


    public App()
    {
        Controller = new Controller( Dispatcher );
    }
}