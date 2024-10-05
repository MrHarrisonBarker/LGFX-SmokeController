namespace LGFX_SmokeController.App.Smoke;

public enum SmokeMachineStatus
{
    Stopped,
    Starting,
    Running,
    Stopping
}

public static class SmokeMachineStatusExtensions
{
    public static string ToStatusString( this SmokeMachineStatus status )
    {
        switch ( status )
        {
            case SmokeMachineStatus.Stopped:
                return "Stopped";
            case SmokeMachineStatus.Starting:
                return "Starting";
            case SmokeMachineStatus.Running:
                return "Running";
            case SmokeMachineStatus.Stopping:
                return "Stopped";
        }

        return string.Empty;
    }
}