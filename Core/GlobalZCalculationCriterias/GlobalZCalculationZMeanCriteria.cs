// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias
{
    /// <summary>
    /// This class calculates the GLobalZ value for each model and also the mean GLobalZ which will help in segregating the trending models from the list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class GlobalZCalculationZMeanCriteria : IGlobalZCalculationCriteria
    {
        public static double MeanGlobalZ;
        public List<T> CalculateGlobalZValue<T>(List<T> trendingModels, List<int> historicalSegmentColumns, List<int> trendingSegmentColumns) where T : TInterface
        {
            MeanGlobalZ = 0.0;
            double sumGlobalZ = 0.0;
            double mean = (double)0.0;
            double standardDeviation = 0.0;
            int count = 0;
            List<int> values = new List<int>();
            double sumHistorySegmentColumns = 0.0;

            //This foreach block calcualtes the sum of all the values in the history segment for all models to calculate the global mean and standard deviation
            foreach (T model in trendingModels)
            {
                foreach (int column in historicalSegmentColumns)
                {
                    sumHistorySegmentColumns += model.CountWithPeriods[column];
                    count++;
                    values.Add(model.CountWithPeriods[column]);
                }
            }

            double countelements = (count * 1.0);
            mean = sumHistorySegmentColumns / countelements;
            standardDeviation = CalculateStandardDeviation(values, mean);

            //In case we meet the corner case of standard deviation coming out to be zero, we set an approximate of standard deviation to be 1 by symmetrical distribution of data
            if (standardDeviation == 0)
                standardDeviation = 1.0;

            //This foreach block calcualtes the globalz values for each value in the trending segment of the models and then takes the mean of them,
            //to calculate the GlobalZ value for the model as well as the mean of all the GlobalZ values
            foreach (T model in trendingModels)
            {
                double sumTempGlobalZ = 0.0;
                foreach (int column in trendingSegmentColumns)
                {
                    sumTempGlobalZ += ((model.CountWithPeriods[column] * 1.0) - mean) / standardDeviation;
                }
                double globalZ = sumTempGlobalZ / trendingSegmentColumns.Count;
                sumGlobalZ += globalZ;
                model.GlobalZ = globalZ;
            }
            MeanGlobalZ = sumGlobalZ / trendingModels.Count;
            return trendingModels;
        }
        double CalculateStandardDeviation(List<int> values, double mean)
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
