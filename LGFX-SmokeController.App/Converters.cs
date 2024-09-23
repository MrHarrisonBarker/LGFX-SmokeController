using System;
using System.Globalization;
using System.Windows.Data;

namespace LGFX_SmokeController.App;

public class PercentageConverter : IValueConverter
{
    public object Convert( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        return ( value?.ToString() ?? "" ) + "%";
    }

    public object ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        return 0;
    }
}