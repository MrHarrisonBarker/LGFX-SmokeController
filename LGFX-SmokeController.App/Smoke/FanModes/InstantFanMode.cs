namespace LGFX_SmokeController.App.Smoke.FanModes;

public class InstantFanMode : FanMode
{
    public InstantFanMode( SmokeMachine machine ) : base( machine )
    {
    }

    public override string Name => "Instant";

    public override Task Start()
    {
        Machine.FanOn = true;
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        Machine.FanOn = false;
        return Task.CompletedTask;
    }
}