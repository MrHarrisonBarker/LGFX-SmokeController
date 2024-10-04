using LGFX_SmokeController.App.Smoke.FanModes;
using LGFX_SmokeController.App.Smoke.SmokeModes;

namespace LGFX_SmokeController.App.Storage.SmokeMachine;

public class StoredSmokeMachine
{
    public int TimeOn { get; set; } = 20;
    public int TimeOff { get; set; } = 20;
    public byte SmokeLevel { get; set; } = 255;
    public byte FanLevel { get; set; } = 255;
    public string SmokeMode { get; set; } = "Timed";
    public string FanMode { get; set; } = "Instant";
    public bool VariableSmoke { get; set; } = true;
    public bool VariableFan { get; set; } = true;
    public short Address { get; set; } = 1;
    public short FanAddress { get; set; } = 2;
    public short HeatAddress { get; set; } = 0;
    public string Name { get; set; } = "1";

    public StoredSmokeMachine( Smoke.SmokeMachine machine )
    {
        TimeOn = machine.TimeOn;
        TimeOff = machine.TimeOff;
        SmokeLevel = machine.SmokeLevel;
        FanLevel = machine.FanLevel;
        SmokeMode = machine.SmokeMode.Name;
        FanMode = machine.FanMode.Name;
        VariableSmoke = machine.VariableSmoke;
        VariableFan = machine.VariableFan;
        Address = machine.Address;
        FanAddress = machine.FanAddress;
        HeatAddress = machine.HeatAddress;
        Name = machine.Name;
    }

    public StoredSmokeMachine()
    {
    }

    public static Smoke.SmokeMachine SmokeMachine( StoredSmokeMachine stored )
    {
        var smokeMachine = new Smoke.SmokeMachine( stored.Name, stored.Address )
        {
            TimeOn = stored.TimeOn,
            TimeOff = stored.TimeOff,
            SmokeLevel = stored.SmokeLevel,
            FanLevel = stored.FanLevel,
            VariableSmoke = stored.VariableSmoke,
            VariableFan = stored.VariableFan,
            FanAddress = stored.FanAddress,
            HeatAddress = stored.HeatAddress
        };

        if ( stored.SmokeMode == "Trigger" )
        {
            smokeMachine.SmokeMode = new TriggerMode( smokeMachine );
        }

        switch ( stored.FanMode )
        {
            case "Instant":
                smokeMachine.FanMode = new InstantFanMode( smokeMachine );
                break;
            case "Timed":
                smokeMachine.FanMode = new TimedFanMode( smokeMachine );
                break;
            case "Constant":
                smokeMachine.FanMode = new ConstantFanMode( smokeMachine );
                break;
        }

        return smokeMachine;
    }
}