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

    #region Underlying Values

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
    private string _Name;
    private FanMode _FanMode;
    private int _TimeOff = 20;
    private int _LeadTime = 0;
    private int _PurgeTime = 20;
    private bool _IsThreeChannel;

    #endregion

    #region Config

    public string Name
    {
        get => _Name;
        set => SetProperty( ref _Name, value );
    }

    public bool IsThreeChannel
    {
        get => _IsThreeChannel;
        set => SetProperty( ref _IsThreeChannel, value );
    }


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
                FanAddress = ( short )( value + 2 );
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

    public FanMode FanMode
    {
        get => _FanMode;
        set => SetProperty( ref _FanMode, value );
    }

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

    public int LeadTime
    {
        get => _LeadTime;
        set => SetProperty( ref _LeadTime, value );
    }

    public int PurgeTime
    {
        get => _PurgeTime;
        set => SetProperty( ref _PurgeTime, value );
    }

    public int TimeOn
    {
        get => _TimeOn;
        set => SetProperty( ref _TimeOn, value );
    }

    public int TimeOff
    {
        get => _TimeOff;
        set => SetProperty( ref _TimeOff, value );
    }

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


    public SmokeMachine( string name, short address, SmokeMachinePresets preset ) : this( name, address )
    {
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
    }

    public SmokeMachine( string name, short address )
    {
        _Name = name;
        Address = address;
        
        _SmokeMode = new TimedMode( this );
        _FanMode = new InstantFanMode( this );
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
        if ( IsOn )
            Stop();
        else
            Trigger();
    }
}