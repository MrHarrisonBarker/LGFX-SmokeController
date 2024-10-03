using CommunityToolkit.Mvvm.ComponentModel;
using LGFX_SmokeController.App.Smoke.FanModes;
using LGFX_SmokeController.App.Smoke.SmokeModes;

namespace LGFX_SmokeController.App.Smoke;

public class SmokeMachine : ObservableObject
{
    public const string STARTING = "Starting";
    public const string RUNNING = "Running";
    public const string STOPPING = "Stopping";
    public const string STOPPED = "Stopped";

    private bool _IsOn;
    private int _TimeOn = 20;
    private string _Status = STOPPED;
    private bool _SmokeOn;
    private bool _FanOn;
    private SmokeMachineMode _SmokeMode;
    private bool _VariableSmoke;
    private bool _VariableFan;
    private short _Address;
    private short _FanAddress;
    private short _HeatAddress;

    #region Config

    public string Name { get; set; }
    public bool IsThreeChannel { get; set; }


    public short HeatAddress
    {
        get => _HeatAddress;
        set => SetProperty( ref _HeatAddress, value );
    }

    public short Address
    {
        get => _Address;
        set
        {
            if ( IsThreeChannel )
            {
                HeatAddress = value;
                SetProperty( ref _Address, ( short )( value - 1 ) );
                FanAddress = ( short )(value + 2);
            }
            else
            {
                SetProperty( ref _Address, value );
                FanAddress = ( short )( value + 1 );
            }
        }
    }

    public short FanAddress
    {
        get => _FanAddress;
        set => SetProperty( ref _FanAddress, value );
    }

    public SmokeMachineMode SmokeMode
    {
        get => _SmokeMode;
        set => SetProperty( ref _SmokeMode, value );
    }

    public FanMode FanMode { get; set; }

    public bool VariableSmoke
    {
        get => _VariableSmoke;
        set => SetProperty( ref _VariableSmoke, value );
    }

    public bool VariableFan
    {
        get => _VariableFan;
        set => SetProperty( ref _VariableFan, value );
    }

    public int LeadTime { get; set; } = 0;
    public int PurgeTime { get; set; } = 20;

    public int TimeOn
    {
        get => _TimeOn;
        set => SetProperty( ref _TimeOn, value );
    }

    public int TimeOff { get; set; } = 20;

    #endregion

    #region State

    public bool IsOn
    {
        get => _IsOn;
        internal set => SetProperty( ref _IsOn, value );
    }

    public string Status
    {
        get => _Status;
        set => SetProperty( ref _Status, value );
    }

    public bool HeatOn { get; set; } = true;

    public bool SmokeOn
    {
        get => _SmokeOn;
        set => SetProperty( ref _SmokeOn, value );
    }

    public bool FanOn
    {
        get => _FanOn;
        set => SetProperty( ref _FanOn, value );
    }


    public byte SmokeLevel { get; set; } = 255;
    public byte FanLevel { get; set; } = 255;


    public byte SmokeValue() => SmokeOn ? SmokeLevel : byte.MinValue;
    public byte FanValue() => FanOn ? FanLevel : byte.MinValue;
    public byte HeatValue() => 255;

    #endregion


    public SmokeMachine( string name, short address, SmokeMachinePresets preset )
    {
        Name = name;
        Address = address;

        switch ( preset )
        {
            case SmokeMachinePresets.Mdg:
                VariableFan = false;
                VariableSmoke = false;
                SmokeLevel = byte.MaxValue;
                FanLevel = byte.MaxValue;
                break;
            case SmokeMachinePresets.Hazer:
                VariableSmoke = true;
                VariableFan = true;
                break;
        }

        SmokeMode = new TimedMode( this );
        FanMode = new InstantFanMode( this );
    }


    public void Trigger()
    {
        SmokeMode.Start();
    }

    public void Stop()
    {
        SmokeMode.Stop();
    }

    public void Toggle()
    {
        Console.WriteLine( $"Toggling machine: {Name}" );
        if ( IsOn )
            Stop();
        else
            Trigger();
    }
}