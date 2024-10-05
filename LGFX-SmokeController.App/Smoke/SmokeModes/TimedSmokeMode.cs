namespace LGFX_SmokeController.App.Smoke.SmokeModes;

public class TimedSmokeMode : SmokeMode
{
    public TimedSmokeMode( SmokeMachine machine ) : base( machine )
    {
    }

    public override string Name => "Timed";

    public override void Start()
    {
        Console.WriteLine($"Starting {Machine.Name}:{Machine.Address} in [TimedMode]");

        Machine.IsOn = true;
        Machine.Status = SmokeMachineStatus.Starting;
        
        Task.Run( async () =>
        {
            while ( !Token.IsCancellationRequested )
            {
                Console.WriteLine( $"Turning on for {Machine.TimeOn}s {Machine.Name}:{Machine.Address} in [TimedMode]" );

                await Machine.FanMode.Start();

                Machine.SmokeOn = true;
                Machine.Status = SmokeMachineStatus.Running;

                await Task.Delay( Machine.TimeOn * 1000, Token );


                Console.WriteLine( $"Running off for {Machine.TimeOff}s {Machine.Name}:{Machine.Address} in [TimedMode]" );

                Machine.SmokeOn = false;

                await Machine.FanMode.Stop();

                await Task.Delay( Machine.TimeOff * 1000, Token );
            }

            Machine.IsOn = false;
        }, Token );
    }
}