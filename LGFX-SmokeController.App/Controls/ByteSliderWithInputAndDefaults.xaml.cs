using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LGFX_SmokeController.App.Controls;

public partial class ByteSliderWithInputAndDefaults : UserControl
{
    public ByteSliderWithInputAndDefaults()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty LabelTextProperty =
        DependencyProperty.Register(nameof(Label), typeof(string), typeof(ByteSliderWithInputAndDefaults),
            new PropertyMetadata(string.Empty));

    public string Label
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public static readonly DependencyProperty NumberProperty =
        DependencyProperty.Register(nameof(Number), typeof(byte), typeof(ByteSliderWithInputAndDefaults),
            new PropertyMetadata(byte.MinValue));

    public byte Number
    {
        get => (byte)GetValue(NumberProperty);
        set => SetValue(NumberProperty, value);
    }

    public static readonly DependencyProperty DefaultProperty = DependencyProperty.Register(nameof(Default),
        typeof(byte), typeof(ByteSliderWithInputAndDefaults), new PropertyMetadata(byte.MinValue));

    public byte Default
    {
        get => (byte)GetValue(DefaultProperty);
        set => SetValue(DefaultProperty, value);
    }

    public static readonly DependencyProperty DefaultsProperty = DependencyProperty.Register(nameof(Defaults),
        typeof(byte[]), typeof(ByteSliderWithInputAndDefaults), new PropertyMetadata(new byte[] { 0, 255 }));

    public byte[] Defaults
    {
        get => (byte[])GetValue(DefaultProperty);
        set => SetValue(DefaultProperty, value);
    }
    
    public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(nameof(Minimum),
        typeof(byte), typeof(ByteSliderWithInputAndDefaults), new PropertyMetadata(byte.MinValue));

    public byte Minimum
    {
        get => (byte)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }
    
    public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof(Maximum),
        typeof(byte), typeof(ByteSliderWithInputAndDefaults), new PropertyMetadata(byte.MaxValue));

    public byte Maximum
    {
        get => (byte)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is MenuItem { DataContext: byte n })
        {
            Number = n;
        }
    }

    private void OnRightClick(object sender, MouseButtonEventArgs e)
    {
        Number = Default;
    }
}