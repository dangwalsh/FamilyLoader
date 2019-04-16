namespace Gensler.Revit.FamilyLoader.Views.ValueConverters
{
    using System.Collections.Generic;
    using System;

    internal class FeetInches
    {
        private int _feet;
        private int _inches;

        public int Feet
        {
            get => _feet;
            private set
            {
                _feet = value;
                if (_feet < 0) _feet = 0;
            }
        }

        public int Inches
        {
            get => _inches;
            private set
            {
                _inches = value;
                if (_inches < 0) _feet = 0;
            }
        }

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

        private void FromFeetAndInches(IReadOnlyList<string> split)
        {
            var feetNumber = Convert.ToInt32(split[0]);
            var inchNumber = Convert.ToInt32(split[1]);
            var inchFeet = inchNumber / 12;
            Feet = feetNumber + inchFeet;
            Inches = inchNumber % 12;
        }

        private void FromDecimalFeet(IReadOnlyList<string> split)
        {
            var number = Convert.ToDouble(split[0]);
            Feet = (int)number;
            Inches = (int)((number - Feet) * 12);
        }

        public double ToDouble()
        {
            return (double) Feet + (double) Inches / 12.0;
        }

        public FeetInches Truncate()
        {
            Inches = 0;
            return this;
        }

        public static FeetInches operator +(FeetInches feetInches, double number)
        {
            var feetNumber = feetInches.ToDouble();

            return new FeetInches(feetNumber + number);
        }

        public static FeetInches operator -(FeetInches feetInches, double number)
        {
            var feetNumber = feetInches.ToDouble();

            return new FeetInches(feetNumber - number);
        }

        
    }
}
