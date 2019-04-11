namespace Gensler.Revit.FamilyLoader.Views.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (TimeSpan) value;
            return time.Hours + ":" + time.Minutes + ":" + time.Seconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
