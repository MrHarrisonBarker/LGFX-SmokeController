using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using LGFX_SmokeController.App.Smoke;

namespace LGFX_SmokeController.App.Settings;

public partial class SmokeSettingsWindow : Window, INotifyPropertyChanged
{
    private bool _IsAddNotOpen = true;
    public App App => ( App )Application.Current;
    
    public int[] SmokeTimingDefaults { get; } = [ 10, 20, 40, 60 ];
    public byte[] AddressDefaults { get; } = [ 1, 3, 5, 7, 9, 11, 13, 15, 17 ];

    public bool IsAddNotOpen
    {
        get => _IsAddNotOpen;
        private set => SetField( ref _IsAddNotOpen, value );
    }

    public SmokeSettingsWindow()
    {
        InitializeComponent();
    }

    private void OnCloseClick( object sender, RoutedEventArgs e )
    {
        Close();
    }

    private void OnAddClick( object sender, RoutedEventArgs e )
    {
        var window = new AddSmokeMachineWindow();
        window.Closed += ( _, _ ) => IsAddNotOpen = true;
        
        window.Show();
        IsAddNotOpen = false;
    }

    private void OnMachineSelected( object sender, SelectionChangedEventArgs e )
    {
        
    }

    private void OnRemoveClick( object sender, RoutedEventArgs e )
    {
        if ( ListOfMachines.SelectedItem is SmokeMachine machine )
        {
            App.SmokeMachines.Remove( machine );
            ListOfMachines.SelectedItem = null;
        }
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
}