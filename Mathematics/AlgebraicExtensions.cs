using System;

namespace DataMining.Learning.Mathematics
{
    public static class AlgebraicExtensions
    {
        public static double ToPowerOf(this double baseNumber, double exponent)
        {
            if (exponent < 0)
                throw new ArgumentOutOfRangeException("exponent");

            checked
            {
                return Math.Pow(baseNumber, exponent);
            }
        }

        public static double Square(this double baseNumber)
        {
            return baseNumber.ToPowerOf(2);
        }

        public static double Cube(this double baseNumber)
        {
            return baseNumber.ToPowerOf(3);
        }

        public static double SquareRoot(this double baseNumber)
        {
            return baseNumber.RootOfDegree(2);
        }

        public static double RootOfDegree(this double baseNumber, double exponent)
        {
            if(exponent < 0)
                throw new ArgumentOutOfRangeException("exponent");

            return Math.Pow(baseNumber, 1/exponent);
        }

        public static double ToAbsolute(this double value)
        {
            return Math.Abs(value);
        }
    }
}
