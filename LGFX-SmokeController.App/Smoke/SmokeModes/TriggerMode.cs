namespace LGFX_SmokeController.App.Smoke.SmokeModes;

public class TriggerMode : SmokeMachineMode
{
    public TriggerMode( SmokeMachine machine ) : base( machine )
    {
    }

    public override void Start()
    {
        Machine.IsOn = true;
        Machine.Status = SmokeMachine.STARTING;

        Machine.FanMode.Start();

        Machine.SmokeOn = true;
        Machine.Status = SmokeMachine.RUNNING;
    }
}