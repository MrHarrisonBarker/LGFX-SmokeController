using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LGFX_SmokeController.App.Controls;

public partial class IntSliderWithInputAndDefaults : UserControl
{
    public IntSliderWithInputAndDefaults()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register( nameof( Label ), typeof( string ), typeof( IntSliderWithInputAndDefaults ),
            new PropertyMetadata( string.Empty ) );

    public string Label
    {
        get => ( string )GetValue( LabelTextProperty );
        set => SetValue( LabelTextProperty, value );
    }

    public static readonly DependencyProperty NumberProperty =
        DependencyProperty.Register( nameof( Number ), typeof( int ), typeof( IntSliderWithInputAndDefaults ),
            new PropertyMetadata( int.MinValue ) );

    public int Number
    {
        get => ( int )GetValue( NumberProperty );
        set => SetValue( NumberProperty, value );
    }

    public static readonly DependencyProperty DefaultProperty = DependencyProperty.Register( nameof( Default ),
        typeof( int ), typeof( IntSliderWithInputAndDefaults ), new PropertyMetadata( ( int )0 ) );

    public int Default
    {
        get => ( int )GetValue( DefaultProperty );
        set => SetValue( DefaultProperty, value );
    }

    public static readonly DependencyProperty DefaultsProperty = DependencyProperty.Register( nameof( Defaults ),
        typeof( int[] ), typeof( IntSliderWithInputAndDefaults ), new PropertyMetadata( new int[] { 0, int.MaxValue / 2, int.MaxValue } ) );

    public int[] Defaults
    {
        get => ( int[] )GetValue( DefaultProperty );
        set => SetValue( DefaultProperty, value );
    }

    public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register( nameof( Minimum ),
        typeof( int ), typeof( IntSliderWithInputAndDefaults ), new PropertyMetadata( ( int )0 ) );

    public int Minimum
    {
        get => ( int )GetValue( MinimumProperty );
        set => SetValue( MinimumProperty, value );
    }

    public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register( nameof( Maximum ),
        typeof( int ), typeof( IntSliderWithInputAndDefaults ), new PropertyMetadata( int.MaxValue ) );

    public int Maximum
    {
        get => ( int )GetValue( MinimumProperty );
        set => SetValue( MinimumProperty, value );
    }

    private void OnClick( object sender, RoutedEventArgs e )
    {
        if ( e.OriginalSource is MenuItem { DataContext: int n } )
        {
            Number = n;
        }
    }

    private void OnRightClick( object sender, MouseButtonEventArgs e )
    {
        Number = Default;
    }
}