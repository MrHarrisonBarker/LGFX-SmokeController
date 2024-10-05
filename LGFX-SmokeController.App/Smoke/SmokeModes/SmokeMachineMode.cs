using CommunityToolkit.Mvvm.ComponentModel;

namespace LGFX_SmokeController.App.Smoke.SmokeModes;

public abstract class SmokeMachineMode : ObservableObject
{
    private CancellationTokenSource TokenSource = new ();
    protected CancellationToken Token => TokenSource.Token;
    public abstract string Name { get; }

    protected readonly SmokeMachine Machine;

    protected SmokeMachineMode( SmokeMachine machine )
    {
        Machine = machine;
    }

    public abstract void Start();

    public virtual void Stop()
    {
        Machine.Status = SmokeMachine.STOPPING;

        Machine.SmokeOn = false;
        Machine.IsOn = false;

        TokenSource.Cancel( false );
        TokenSource = new CancellationTokenSource();

        Task.Run( () =>
        {
            Machine.FanMode.Stop();

            Machine.Status = SmokeMachine.STOPPED;
        }, Token );
    }

    public void StopImmediately()
    {
        TokenSource.Cancel( false );
        TokenSource = new CancellationTokenSource();
        Machine.SmokeOn = false;
        Machine.FanMode.StopImmediately();
        Machine.Status = SmokeMachine.STOPPED;
    }
}