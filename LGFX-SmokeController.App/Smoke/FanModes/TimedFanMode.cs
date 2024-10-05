namespace LGFX_SmokeController.App.Smoke.FanModes;

public class TimedFanMode : FanMode
{
    public TimedFanMode( SmokeMachine machine ) : base( machine )
    {
    }

    public override string Name => "Timed";
    public override void Start()
    {
        Machine.FanOn = true;
        
        Thread.Sleep( Machine.FanLeadTime * 1000 );
    }

    public override void Stop()
    {
        Thread.Sleep( Machine.FanPurgeTime * 1000 );

        Machine.FanOn = false;
    }
}