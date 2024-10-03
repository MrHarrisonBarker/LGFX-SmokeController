using CommunityToolkit.Mvvm.ComponentModel;

namespace LGFX_SmokeController.App.Smoke.SmokeModes;

public abstract class SmokeMachineMode : ObservableObject
{
    protected CancellationTokenSource TokenSource = new ();
    protected CancellationToken Token => TokenSource.Token;
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

        Machine.FanMode.Stop();

        Machine.Status = SmokeMachine.STOPPED;
    }
}