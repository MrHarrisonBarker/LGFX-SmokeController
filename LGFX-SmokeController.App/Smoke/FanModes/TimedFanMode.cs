namespace LGFX_SmokeController.App.Smoke.FanModes;

public class TimedFanMode : FanMode
{
    public TimedFanMode( SmokeMachine machine ) : base( machine )
    {
    }

    public override string Name => "Timed";
    public override void Start()
    {
        throw new NotImplementedException();
    }

    public override void Stop()
    {
        throw new NotImplementedException();
    }
}