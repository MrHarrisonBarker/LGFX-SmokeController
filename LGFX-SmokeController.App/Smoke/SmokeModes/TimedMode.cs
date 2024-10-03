namespace LGFX_SmokeController.App.Smoke.SmokeModes;

public class TimedMode : SmokeMachineMode
{
    public TimedMode( SmokeMachine machine ) : base( machine )
    {
    }

    public override void Start()
    {
        Console.WriteLine($"Starting {Machine.Name}:{Machine.Address} in [TimedMode]");

        Machine.IsOn = true;
        Machine.Status = SmokeMachine.STARTING;
        
        Task.Run( async () =>
        {
            while ( !Token.IsCancellationRequested )
            {
                Console.WriteLine( $"Turning on for {Machine.TimeOn}s {Machine.Name}:{Machine.Address} in [TimedMode]" );

                Machine.FanMode.Start();

                Machine.SmokeOn = true;
                Machine.Status = SmokeMachine.RUNNING;

                await Task.Delay( Machine.TimeOn * 1000, Token );


                Console.WriteLine( $"Running off for {Machine.TimeOff}s {Machine.Name}:{Machine.Address} in [TimedMode]" );

                Machine.SmokeOn = false;

                Machine.FanMode.Stop();

                await Task.Delay( Machine.TimeOff * 1000, Token );
            }

            Machine.IsOn = false;
        }, Token );
    }
}