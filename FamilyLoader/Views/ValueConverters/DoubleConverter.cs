using System.Collections.Generic;
using System.Linq;

namespace Gensler.Revit.FamilyLoader.Views.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(double), typeof(string))]
    internal class DoubleConverter : IValueConverter
    {
        private readonly char[] _charsToRemove = { '"', '*', '/', '-', '+', '{', '}' };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) value = 0;
            var number = (double)value;
            var feetInch = ConvertDouble(number);
            return feetInch;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) value = "0";
            var text = (string)value;
            var numbers = text.Replace("'", "").Trim(_charsToRemove);
            var feetInch = ConvertText(numbers);
            return feetInch;
        }

        private static double ConvertText(string text)
        {
            var split = text?.Split(' ')
                .Where(x => int.TryParse(x, out var num))
                .ToArray();

            switch (split?.Length)
            {
                case 1:
                    return FromDecimalFeet(split);
                default:
                    return FromFeetAndInches(split);
            }
        }

        private static string ConvertDouble(double number)
        {
            var feet = (int)number;
            var inches = (int)((number - feet) * 12.0);

            return feet.ToString() + "' " + inches.ToString() + "\"";
        }

        private static double FromFeetAndInches(IReadOnlyList<string> split)
        {
            var feetNumber = System.Convert.ToDouble(split[0]);
            var inchNumber = System.Convert.ToDouble(split[1]);
            var inchFeet = inchNumber / 12.0;
            var feet = feetNumber + inchFeet;

            return feet;
        }

        private static double FromDecimalFeet(IReadOnlyList<string> split)
        {
            var number = System.Convert.ToDouble(split[0]);

            return number;
        }
    }
}
