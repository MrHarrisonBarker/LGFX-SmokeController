namespace LGFX_SmokeController.App.Smoke.FanModes;

public class TimedFanMode : FanMode
{
    public TimedFanMode( SmokeMachine machine ) : base( machine )
    {
    }

    public override string Name => "Timed";
    public override async Task Start()
    {
        Machine.FanOn = true;
        
        await Task.Delay( Machine.FanLeadTime * 1000, Token );
    }

    public override async Task Stop()
    {
        await Task.Delay( Machine.FanPurgeTime * 1000, Token );

        Machine.FanOn = false;
    }
}