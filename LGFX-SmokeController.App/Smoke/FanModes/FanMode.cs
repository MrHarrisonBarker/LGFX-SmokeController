using CommunityToolkit.Mvvm.ComponentModel;

namespace LGFX_SmokeController.App.Smoke.FanModes;

public abstract class FanMode : ObservableObject
{
    public abstract string Name { get; }
    protected readonly SmokeMachine Machine;

    protected FanMode( SmokeMachine machine )
    {
        Machine = machine;
    }

    public abstract void Start();
    public abstract void Stop();
}