namespace Gensler.Revit.FamilyLoader.Views.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    internal class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) 
                return new TimeSpan(0, 0, 0).ToString("h\\:mm\\:ss");

            var time = (TimeSpan) value;
            return time.ToString("h\\:mm\\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
