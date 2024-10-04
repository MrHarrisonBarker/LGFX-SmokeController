namespace LGFX_SmokeController.App.Smoke.FanModes;

public class ConstantFanMode : FanMode
{
    public ConstantFanMode( SmokeMachine machine ) : base( machine )
    {
        machine.FanOn = true;
    }

    public override string Name => "Constant";
    public override void Start()
    {
        Machine.FanOn = true;
    }

    public override void Stop()
    {
        Machine.FanOn = true;
    }
}