namespace LGFX_SmokeController.App.Smoke.FanModes;

public class InstantFanMode : FanMode
{
    public InstantFanMode( SmokeMachine machine ) : base( machine )
    {
    }

    public override void Start()
    {
        Machine.FanOn = true;
    }

    public override void Stop()
    {
        Machine.FanOn = false;
    }
}