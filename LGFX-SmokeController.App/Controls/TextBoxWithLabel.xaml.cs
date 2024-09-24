using System.Windows;
using System.Windows.Controls;

namespace LGFX_SmokeController.App.Controls;

public partial class TextBoxWithLabel : UserControl
{
    public TextBoxWithLabel()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register( nameof( Label ), typeof( string ), typeof( TextBoxWithLabel ), new PropertyMetadata( string.Empty ) );

    public string Label
    {
        get => ( string )GetValue( LabelTextProperty );
        set => SetValue( LabelTextProperty, value );
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register( nameof( Text ), typeof( string ), typeof( TextBoxWithLabel ), new PropertyMetadata( string.Empty ) );

    public string Text
    {
        get => ( string )GetValue( TextProperty );
        set => SetValue( TextProperty, value );
    }
}