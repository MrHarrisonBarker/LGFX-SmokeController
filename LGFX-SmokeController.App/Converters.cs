﻿using System;
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

[ValueConversion( typeof( bool ), typeof( bool ) )]
public class InverseBooleanConverter : IValueConverter
{
    #region IValueConverter Members

    public object Convert( object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture )
    {
        if ( targetType != typeof( bool ) )
            throw new InvalidOperationException( "The target must be a boolean" );

        return !( bool )value;
    }

    public object ConvertBack( object value, Type targetType, object parameter,
        System.Globalization.CultureInfo culture )
    {
        throw new NotSupportedException();
    }

    #endregion
}