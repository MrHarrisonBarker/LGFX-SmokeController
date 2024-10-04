using System.IO;

namespace LGFX_SmokeController.App.Storage;

public static class Files
{
    private static string Directory => Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "LGFXSmokeControl/" );
    public static readonly string SmokeMachines = Path.Combine( Files.Directory, "smoke_machines.xml" );
    public static readonly string Network = Path.Combine( Files.Directory, "network.xml" );
}