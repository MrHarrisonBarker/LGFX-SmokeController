using CommunityToolkit.Mvvm.ComponentModel;
using LGFX_SmokeController.App.Smoke.FanModes;
using LGFX_SmokeController.App.Smoke.SmokeModes;

namespace LGFX_SmokeController.App.Smoke;

public class SmokeMachine : ObservableObject
{
    public const byte MaxLevel = 100;
    public const byte MinLevel = 0;

    public const int DefaultTimeOn = 20;
    public const int DefaultTimeOff = 20;
    public const int DefaultFanLeadTime = 2;
    public const int DefaultFanPurgeTime = 2;

    #region Underlying Values

    private bool _IsOn;
    private int _TimeOn = DefaultTimeOn;
    private SmokeMachineStatus _Status = SmokeMachineStatus.Stopped;
    private bool _SmokeOn;
    private bool _FanOn;
    private SmokeMode _SmokeMode;
    private bool _VariableSmoke;
    private bool _VariableFan;
    private short _Address;
    private short _FanAddress;
    private short _HeatAddress;
    private string _Name;
    private FanMode _FanMode;
    private int _TimeOff = DefaultTimeOff;
    private int _FanLeadTime = DefaultFanLeadTime;
    private int _FanPurgeTime = DefaultFanPurgeTime;
    private bool _IsThreeChannel;
    private byte _SmokeLevel = MaxLevel;
    private byte _FanLevel = MaxLevel;

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

    public SmokeMode SmokeMode
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

    public int FanLeadTime
    {
        get => _FanLeadTime;
        set => SetProperty( ref _FanLeadTime, value );
    }

    public int FanPurgeTime
    {
        get => _FanPurgeTime;
        set => SetProperty( ref _FanPurgeTime, value );
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

    public SmokeMachineStatus Status
    {
        get => _Status;
        set => SetProperty( ref _Status, value );
    }

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


    public byte SmokeLevel
    {
        get => _SmokeLevel;
        set => SetProperty( ref _SmokeLevel, value );
    }

    public byte FanLevel
    {
        get => _FanLevel;
        set => SetProperty( ref _FanLevel, value );
    }

    public static byte Map( byte input, byte fromStart, byte fromEnd, byte toStart, byte toEnd )
    {
        return ( byte )( toStart + ( input - fromStart ) * ( toEnd - toStart ) / ( fromEnd - fromStart ) );
    }

    public byte SmokeValue() => SmokeOn ? Map( SmokeLevel, MinLevel, MaxLevel, byte.MinValue, byte.MaxValue ) : byte.MinValue;
    public byte FanValue() => FanOn ? Map( FanLevel, MinLevel, MaxLevel, byte.MinValue, byte.MaxValue ) : byte.MinValue;

    #endregion


    public SmokeMachine( string name, short address, SmokeMachinePresets preset ) : this( name, address )
    {
        switch ( preset )
        {
            case SmokeMachinePresets.Mdg:
                VariableFan = false;
                VariableSmoke = false;
                SmokeLevel = MaxLevel;
                FanLevel = MaxLevel;
                break;
            case SmokeMachinePresets.Hazer:
                VariableSmoke = true;
                VariableFan = true;
                break;
            case SmokeMachinePresets.Viper:
                VariableSmoke = true;
                VariableFan = false;
                FanLevel = MaxLevel;
                break;
        }
    }

    public SmokeMachine( string name, short address )
    {
        _Name = name;
        Address = address;

        _SmokeMode = new TimedSmokeMode( this );
        _FanMode = new TimedFanMode( this );
    }


    public void Trigger()
    {
        if ( !IsOn && Status is SmokeMachineStatus.Stopping )
        {
            StopImmediately();
            SmokeMode.Start();
        }
        else if ( !IsOn && Status is not SmokeMachineStatus.Starting ) 
            SmokeMode.Start();
        
    }

    public void Stop()
    {
        if ( IsOn && ( Status is SmokeMachineStatus.Starting or SmokeMachineStatus.Stopping ) )
            StopImmediately();
        else if ( IsOn && Status != SmokeMachineStatus.Stopping ) 
            SmokeMode.Stop();
    }

    public void StopImmediately()
    {
        SmokeMode.StopImmediately();
    }

    public void Toggle()
    {
        if ( IsOn )
            Stop();
        else
            Trigger();
    }
}