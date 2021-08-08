using System;
using System.Collections.Generic;
using System.Text;

namespace TrendsCalculator.Library.Helper
{
    internal class CalculationHelper
    {
        internal double CalculateStandardDeviation(List<int> values, double mean)
        {
            double standardDeviation = 0.0;
            double summation = 0.0;
            foreach (int value in values)
            {
                summation += Math.Pow((value * 1.0) - mean, 2);
            }
            summation = summation / values.Count;
            standardDeviation = Math.Sqrt(summation);
            return standardDeviation;
        }
    }
}
