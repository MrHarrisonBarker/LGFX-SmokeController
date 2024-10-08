﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.Settings;
using LGFX_SmokeController.App.Smoke;
using Microsoft.Win32;

namespace LGFX_SmokeController.App.Main;

public partial class MainMenu : UserControl, INotifyPropertyChanged
{
    private Controller Controller => ( Controller )DataContext;

    private bool _IsSettingsWindowNotOpen = true;
    private bool _IsSmokeSettingsWindowNotOpen = true;

    public bool IsSettingsWindowNotOpen
    {
        get => _IsSettingsWindowNotOpen;
        set => SetField( ref _IsSettingsWindowNotOpen, value );
    }

    public bool IsSmokeSettingsWindowNotOpen
    {
        get => _IsSmokeSettingsWindowNotOpen;
        set => SetField( ref _IsSmokeSettingsWindowNotOpen, value );
    }

    public MainMenu()
    {
        InitializeComponent();
    }

    private void OnSettingsClick( object sender, RoutedEventArgs e )
    {
        IsSettingsWindowNotOpen = false;
        var window = new SettingsWindow( Controller );
        window.Closed += ( _, _ ) => IsSettingsWindowNotOpen = true;
        window.Show();
    }

    private void OnSmokeSettingsClick( object sender, RoutedEventArgs e )
    {
        IsSmokeSettingsWindowNotOpen = false;
        var window = new SmokeSettingsWindow( Controller );
        window.Closed += ( _, _ ) => IsSmokeSettingsWindowNotOpen = true;
        window.Show();
    }

    private void OnDefaultPresetClick( object sender, RoutedEventArgs e )
    {
    }

    private void OnSavePresetClick( object sender, RoutedEventArgs e )
    {
        var dialog = new SaveFileDialog()
        {
            DefaultExt = LgfxSmokeFileExtension.Extension,
            Filter = LgfxSmokeFileExtension.Filter
        };

        dialog.ShowDialog();
    }

    private void OnOpenPresetClick( object sender, RoutedEventArgs e )
    {
        var dialog = new OpenFileDialog
        {
            Multiselect = false,
            Title = "Open Preset",
            CheckFileExists = true,
            Filter = LgfxSmokeFileExtension.Filter,
            DefaultExt = LgfxSmokeFileExtension.Extension
        };

        dialog.ShowDialog();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged( [CallerMemberName] string? propertyName = null )
    {
        PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
    }

    protected bool SetField<T>( ref T field, T value, [CallerMemberName] string? propertyName = null )
    {
        if ( EqualityComparer<T>.Default.Equals( field, value ) ) return false;
        field = value;
        OnPropertyChanged( propertyName );
        return true;
    }

    private void OnFestivalPresetClick( object sender, RoutedEventArgs e )
    {
        Controller.ClearMachines();

        Controller.SetMachines(
            new SmokeMachine( "SL 1", 1, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "SL 2", 3, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "SR 3", 5, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "SR 4", 7, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "FOH 4", 9, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "FOH 5", 11, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "DELAY 6", 13, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "DELAY 7", 15, SmokeMachinePresets.Hazer )
        );

        Controller.Save();
    }

    private void OnMDGFestivalPresetClick( object sender, RoutedEventArgs e )
    {
        Controller.ClearMachines();

        Controller.SetMachines(
            new SmokeMachine( "SL 1", 1, SmokeMachinePresets.Mdg ),
            new SmokeMachine( "SL 2", 3, SmokeMachinePresets.Mdg ),
            new SmokeMachine( "SR 3", 5, SmokeMachinePresets.Mdg ),
            new SmokeMachine( "SR 4", 7, SmokeMachinePresets.Mdg ),
            new SmokeMachine( "FOH 4", 9, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "FOH 5", 11, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "DELAY 6", 13, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "DELAY 7", 15, SmokeMachinePresets.Hazer )
        );

        Controller.Save();
    }

    private void OnHAZERSPresetClick( object sender, RoutedEventArgs e )
    {
        Controller.ClearMachines();

        Controller.SetMachines(
            new SmokeMachine( "HAZER 1", 1, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "HAZER 2", 3, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "HAZER 3", 5, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "HAZER 4", 7, SmokeMachinePresets.Hazer )
        );

        Controller.Save();
    }

    private void OnSaveClick( object sender, RoutedEventArgs e ) => Controller.Save();

    private void OnNewClick( object sender, RoutedEventArgs e ) => Controller.New();

    private void OnBoomtownPresetClick( object sender, RoutedEventArgs e )
    {
        Controller.ClearMachines();

        Controller.SetMachines(
            new SmokeMachine( "SL 1", 1, SmokeMachinePresets.Mdg ),
            new SmokeMachine( "SL 2", 3, SmokeMachinePresets.Mdg ),
            new SmokeMachine( "SR 3", 5, SmokeMachinePresets.Mdg ),
            new SmokeMachine( "SR 4", 7, SmokeMachinePresets.Mdg ),
            new SmokeMachine( "FOH 4", 9, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "FOH 5", 11, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "BAR 6", 13, SmokeMachinePresets.Viper ),
            new SmokeMachine( "BAR 7", 15, SmokeMachinePresets.Viper ),
            new SmokeMachine( "DELAY 8", 17, SmokeMachinePresets.Hazer ),
            new SmokeMachine( "DELAY 9", 19, SmokeMachinePresets.Hazer )
        );

        Controller.Save();
    }
}