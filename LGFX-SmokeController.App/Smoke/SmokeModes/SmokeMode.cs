using CommunityToolkit.Mvvm.ComponentModel;

namespace LGFX_SmokeController.App.Smoke.SmokeModes;

public abstract class SmokeMode : ObservableObject
{
    private CancellationTokenSource TokenSource = new ();
    protected CancellationToken Token => TokenSource.Token;
    public abstract string Name { get; }

    protected readonly SmokeMachine Machine;

    protected SmokeMode( SmokeMachine machine )
    {
        Machine = machine;
    }

    public abstract void Start();

    public virtual void Stop()
    {
        Machine.Status = SmokeMachineStatus.Stopping;

        Machine.SmokeOn = false;
        Machine.IsOn = false;

        TokenSource.Cancel( false );
        TokenSource = new CancellationTokenSource();

        Task.Run( async () =>
        {
            await Machine.FanMode.Stop();

            Machine.Status = SmokeMachineStatus.Stopped;
        }, Token );
    }

    public void StopImmediately()
    {
        Machine.IsOn = false;
        TokenSource.Cancel( false );
        TokenSource = new CancellationTokenSource();
        Machine.SmokeOn = false;
        Machine.FanMode.StopImmediately();
        Machine.Status = SmokeMachineStatus.Stopped;
    }
}