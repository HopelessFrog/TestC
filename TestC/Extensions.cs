﻿namespace TestingIntegralX
{
    internal static class Extensions
    {
        public static double NextDoubleLogarithmic(this Random random, double minValue, double maxValue)
        {
            bool posAndNeg = minValue < 0d && maxValue > 0d;
            double minAbs = Math.Min(Math.Abs(minValue), Math.Abs(maxValue));
            double maxAbs = Math.Max(Math.Abs(minValue), Math.Abs(maxValue));

            int sign;
            if (!posAndNeg)
                sign = minValue < 0d ? -1 : 1;
            else
            {

                double sample = random.NextDouble();
                var rate = minAbs / maxAbs;
                var absMinValue = Math.Abs(minValue);
                bool isNeg = absMinValue <= maxValue ? rate / 2d > sample : rate / 2d < sample;
                sign = isNeg ? -1 : 1;


                minAbs = 0d;
                maxAbs = isNeg ? absMinValue : Math.Abs(maxValue);
            }


            double minExponent = minAbs == 0d ? -16d : Math.Log(minAbs, 2d);
            double maxExponent = Math.Log(maxAbs, 2d);
            if (minExponent == maxExponent)
                return minValue;


            if (maxExponent < minExponent)
                minExponent = maxExponent - 4;

            double result = sign * Math.Pow(2d, NextDoubleLinear(random, minExponent, maxExponent));


            return result < minValue ? minValue : (result > maxValue ? maxValue : result);
        }

        public static double NextDoubleLinear(this Random random, double minValue, double maxValue)
        {

            double sample = random.NextDouble();
            return (maxValue * sample) + (minValue * (1d - sample));
        }
    }
}
