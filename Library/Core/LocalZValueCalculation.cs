// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.AlgoComponents
{
    /// <summary>
    /// This class calculates the value of LocalZ for each of the models
    /// </summary>
    /// <typeparam name="T">TInterface to adhere to</typeparam>
    internal class LocalZValueCalculation<T> where T : TInternal
    {
        internal List<T> CalcualteLocalZValue(List<T> trendingModels, List<int> historicalSegmentColumns, List<int> trendingSegmentColumns)
        {
            //The ForEach block calculates the sum of the entries in the history segment of countWithPeriods of each model,
            //to aid in calculation of mean and standard deviation for  each model
            foreach (T model in trendingModels)
            {
                double sumHistorySegmentColumn = 0.0;
                List<int> historicalSegmentValues = new List<int>();
                foreach (int column in historicalSegmentColumns)
                {
                    sumHistorySegmentColumn += model.CountWithPeriods[column];
                    historicalSegmentValues.Add(model.CountWithPeriods[column]);

                }

                double mean = (sumHistorySegmentColumn * 1.0) / historicalSegmentColumns.Count;

                double standardDeviation = CalculateStandardDeviation(historicalSegmentValues, mean);

                //In case we meet the corner case of standard deviation coming out to be zero, we set an approximate of standard deviation to be 1 by symmetrical distribution of data
                if (standardDeviation == 0)
                    standardDeviation = 1.0;

                //This ForEach block calculates the LocalZ values for each column in the trending segment and then finally calcualtes the mean of them
                //to account for the LocalZ value for a model
                double sumTempLocalZ = 0.0;
                foreach (int column in trendingSegmentColumns)
                {
                    sumTempLocalZ += (((model.CountWithPeriods[column] - mean) * 1.0) / standardDeviation);
                }

                double LocalZ = sumTempLocalZ / trendingSegmentColumns.Count;

                model.LocalZ = LocalZ;
            }
            return trendingModels;
        }
        //Function to calculate the Standard Deviation
        private double CalculateStandardDeviation(List<int> values, double mean)
        {
            double standardDeviation = 0.0;
            double summation = 0.0;
            foreach (int value in values)
            {
                summation += Math.Pow(((value * 1.0) - mean), 2);
            }
            summation = summation / values.Count;
            standardDeviation = Math.Sqrt(summation);
            return standardDeviation;
        }
    }
}
