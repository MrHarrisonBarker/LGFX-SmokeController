using CommunityToolkit.Mvvm.ComponentModel;

namespace LGFX_SmokeController.App.Smoke.FanModes;

public abstract class FanMode : ObservableObject
{
    private CancellationTokenSource TokenSource = new ();
    protected CancellationToken Token => TokenSource.Token;
    public abstract string Name { get; }
    protected readonly SmokeMachine Machine;

    protected FanMode( SmokeMachine machine )
    {
        Machine = machine;
    }

    public abstract Task Start();
    public abstract Task Stop();

    public void StopImmediately()
    {
        TokenSource.Cancel( false );
        TokenSource = new CancellationTokenSource();
        Machine.FanOn = false;
    }
}