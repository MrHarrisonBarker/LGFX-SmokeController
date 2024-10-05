namespace LGFX_SmokeController.App.Smoke.SmokeModes;

public class TriggerMode : SmokeMachineMode
{
    public TriggerMode( SmokeMachine machine ) : base( machine )
    {
    }

    public override string Name => "Trigger";

    public override void Start()
    {
        Machine.IsOn = true;
        Machine.Status = SmokeMachineStatus.Starting;

        Task.Run( async () =>
        {
            await Machine.FanMode.Start();

            Machine.SmokeOn = true;
            Machine.Status = SmokeMachineStatus.Running;
        }, Token );
        
    }
}