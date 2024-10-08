﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.Smoke;
using LGFX_SmokeController.App.Smoke.FanModes;

namespace LGFX_SmokeController.App.Settings;

public partial class SmokeSettingsWindow : Window, INotifyPropertyChanged
{
    private bool _IsAddNotOpen = true;
    private Controller Controller => ( Controller )DataContext;

    public int[] SmokeTimingDefaults { get; } = [ 10, 20, 40, 60 ];
    public byte[] AddressDefaults { get; } = [ 1, 3, 5, 7, 9, 11, 13, 15, 17 ];

    public string[] FanModes { get; } = [ "Instant", "Timed", "Constant" ];

    public bool IsAddNotOpen
    {
        get => _IsAddNotOpen;
        private set => SetField( ref _IsAddNotOpen, value );
    }

    public SmokeSettingsWindow( Controller controller )
    {
        DataContext = controller;
        InitializeComponent();
        Closed += ( _, _ ) =>
        {
            Controller.SelectedMachine = null;
            Controller.Save();
        };
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        Close();
    }

    private void OnAddClick( object sender, RoutedEventArgs e )
    {
        var window = new AddSmokeMachineWindow( Controller );
        window.Closed += ( _, _ ) => IsAddNotOpen = true;

        window.Show();
        IsAddNotOpen = false;
    }

    private void OnMachineSelected( object sender, SelectionChangedEventArgs e )
    {
        if ( sender is ListBox listBox )
        {
            Controller.SelectedMachine = ( SmokeMachine? )listBox.SelectedItem;

            if ( listBox.SelectedItem is SmokeMachine machine )
            {
                FanModeComboBox.SelectedItem = machine.FanMode.Name;
            }
        }
    }

    private void OnRemoveClick( object sender, RoutedEventArgs e )
    {
        if ( ListOfMachines.SelectedItem is SmokeMachine machine )
        {
            Controller.SmokeMachines.Remove( machine );
            ListOfMachines.SelectedItem = null;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged( [CallerMemberName] string? propertyName = null )
    {
        PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
    }

    private bool SetField<T>( ref T field, T value, [CallerMemberName] string? propertyName = null )
    {
        if ( EqualityComparer<T>.Default.Equals( field, value ) ) return false;
        field = value;
        OnPropertyChanged( propertyName );
        return true;
    }

    private void OnFanModeChange( object sender, SelectionChangedEventArgs e )
    {
        if ( sender is ComboBox comboBox && ListOfMachines.SelectedItem is SmokeMachine machine )
        {
            if ( comboBox.SelectedItem as string != machine.FanMode.Name )
            {
                machine.StopImmediately();

                switch ( comboBox.SelectedItem as string )
                {
                    case "Instant":
                        machine.FanMode = new InstantFanMode( machine );
                        break;
                    case "Timed":
                        machine.FanMode = new TimedFanMode( machine );
                        break;
                    case "Constant":
                        machine.FanMode = new ConstantFanMode( machine );
                        break;
                }
            }
        }
    }

    private void OnMakeMDGClick( object sender, RoutedEventArgs e )
    {
        if ( ListOfMachines.SelectedItem is SmokeMachine machine )
        {
            machine.VariableFan = false;
            machine.VariableSmoke = false;
            machine.SmokeLevel = SmokeMachine.MaxLevel;
            machine.FanLevel = SmokeMachine.MaxLevel;
        }
    }

    private void OnMakeHAZERClick( object sender, RoutedEventArgs e )
    {
        if ( ListOfMachines.SelectedItem is SmokeMachine machine )
        {
            machine.VariableFan = true;
            machine.VariableSmoke = true;
        }
    }

    private void OnMakeVIPERClick( object sender, RoutedEventArgs e )
    {
        if ( ListOfMachines.SelectedItem is SmokeMachine machine )
        {
            machine.VariableFan = false;
            machine.FanLevel = SmokeMachine.MaxLevel;
            machine.VariableSmoke = true;
        }
    }
}