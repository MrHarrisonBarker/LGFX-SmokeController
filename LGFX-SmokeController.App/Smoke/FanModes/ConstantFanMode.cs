namespace LGFX_SmokeController.App.Smoke.FanModes;

public class ConstantFanMode : FanMode
{
    public ConstantFanMode( SmokeMachine machine ) : base( machine )
    {
        machine.FanOn = true;
    }

    public override string Name => "Constant";
    public override Task Start()
    {
        Machine.FanOn = true;
        return Task.CompletedTask;
    }

    public override Task Stop()
    {
        Machine.FanOn = true;
        return Task.CompletedTask;
    }
}