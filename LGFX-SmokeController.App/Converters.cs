using System.Globalization;
using System.Windows.Data;

namespace LGFX_SmokeController.App;

[ValueConversion( typeof( byte ), typeof( string ) )]
public class PercentageConverter : IValueConverter
{
    public object Convert( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        if ( value is byte v )
        {
            return $"{v}%";
        }

        throw new NotImplementedException();
    }

    public object ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        throw new NotImplementedException();
    }
}

public class MultiValueEqualityConverter : IMultiValueConverter
{
    public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
    {
        return values?.All( o => o?.Equals( values[ 0 ] ) == true ) == true || values?.All( o => o == null ) == true;
    }

    public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
    {
        return [ ];
    }
}

[ValueConversion( typeof( object ), typeof( bool ) )]
public class IsNullConverter : IValueConverter
{
    public object Convert( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        return value is null;
    }

    public object ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        throw new NotSupportedException();
    }
}

[ValueConversion( typeof( object ), typeof( bool ) )]
public class IsNotNullConverter : IValueConverter
{
    public object Convert( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        return value is not null;
    }

    public object ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture )
    {
        throw new NotSupportedException();
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