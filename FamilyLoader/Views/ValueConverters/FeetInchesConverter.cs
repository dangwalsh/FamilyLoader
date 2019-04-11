namespace Gensler.Revit.FamilyLoader.Views.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(string), typeof(string))]
    internal class FeetInchesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var feetInch = (FeetInches) value;
            return feetInch?.Feet + "' " + feetInch?.Inches + "\"";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (string) value;
            var feetInch = new FeetInches(text);
            return feetInch;
        }
    }
}
