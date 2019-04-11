namespace Gensler.Revit.FamilyLoader.Views.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(double), typeof(string))]
    internal class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) value = 0;
            var number = (double)value;
            var feetInch = new FeetInches(number);
            return feetInch?.Feet + "' " + feetInch?.Inches + "\"";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) value = 0;
            var number = (string)value;
            return System.Convert.ToDouble(number);
        }
    }
}
