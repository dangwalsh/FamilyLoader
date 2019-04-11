namespace Gensler.Revit.FamilyLoader.Views.ValueConverters
{
    using System;

    internal class FeetInches
    {
        public int Feet { get; private set; }
        public int Inches { get; private set; }

        public FeetInches()
        {
            Feet = 0;
            Inches = 0;
        }

        public FeetInches(string text)
        {
            var split = text?.Split(' ');
            switch (split?.Length)
            {
                case 1:
                    FromDecimalFeet(split);
                    break;
                case 2:
                    FromFeetAndInches(split);
                    break;
                default:
                    throw new Exception("Too many values.");
            }
        }

        public FeetInches(double number)
        {
            FromDouble(number);
        }

        private void FromDouble(double number)
        {
            var feetNumber = Math.Truncate(number);
            var inchNumber = number - feetNumber;
            Feet = (int) feetNumber;
            Inches = (int) (inchNumber * 12);
        }

        private void FromFeetAndInches(string[] split)
        {
            var feetNumber = Convert.ToInt32(split[0]);
            var inchNumber = Convert.ToInt32(split[1]);
            var inchFeet = inchNumber / 12;
            Feet = feetNumber + inchFeet;
            Inches = inchNumber % 12;
        }

        private void FromDecimalFeet(string[] split)
        {
            var number = Convert.ToDouble(split[0]);
            Feet = (int)number;
            Inches = (int)((number - Feet) * 12);
        }
    }
}
