namespace LGFX_SmokeController.App.Smoke;

public class SmokeMachine
{
    #region Config

    public string Name { get; set; }
    public bool IsThreeChannel { get; set; }


    public short HeatAddress { get; set; }
    public short Address { get; set; }
    public short FanAddress { get; set; }

    public bool VariableSmoke { get; set; }
    public bool VariableFan { get; set; }

    #endregion

    #region State

    public bool HeatOn { get; set; } = true;
    public bool SmokeOn { get; set; }
    public bool FanOn { get; set; }


    public byte SmokeLevel { get; set; }
    public byte FanLevel { get; set; }


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
    }


    public void Trigger()
    {
    }

    public void Stop()
    {
        
    }
}