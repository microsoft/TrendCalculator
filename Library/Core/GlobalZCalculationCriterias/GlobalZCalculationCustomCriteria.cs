// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias
{
    /// <summary>
    /// This class calculates the GLobalZ value for each model 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class GlobalZCalculationCustomCriteria : IGlobalZCalculationCriteria
    {
        public List<(T item, double localZ, double globalZ)> CalculateGlobalZValue<T>(List<(T item, double localZ, double globalZ)> trendingModels, List<int> historicalSegmentColumns, List<int> trendingSegmentColumns) where T: TModel
        {
            double mean = (double)0.0;
            double standardDeviation = 0.0;
            int count = 0;
            List<int> values = new List<int>();
            double sumHistorySegmentColumns = 0.0;

            //This for each block calculates the sum of all the values in the history segment for all models to calculate the global mean and standard deviation
            foreach (var model in trendingModels)
            {
                foreach (int column in historicalSegmentColumns)
                {
                    sumHistorySegmentColumns += model.item.CountWithPeriods[column];
                    count++;
                    values.Add(model.item.CountWithPeriods[column]);
                }
            }

            double countelements = (count * 1.0);
            mean = sumHistorySegmentColumns / countelements;
            standardDeviation = CalculateStandardDeviation(values, mean);

            //In case we meet the corner case of standard deviation coming out to be zero, we set an approximate of standard deviation to be 1 by symmetrical distribution of data
            if (standardDeviation == 0)
                standardDeviation = 1.0;

            //This for each block calculates the globalZ values for each value in the trending segment of the models and then takes the mean of them,
            //to calculate the GlobalZ value for the model 
            for(int increment = 0;increment < trendingModels.Count; increment++)
            {
                double sumTempGlobalZ = 0.0;
                foreach (int column in trendingSegmentColumns)
                {
                    sumTempGlobalZ += ((trendingModels[increment].item.CountWithPeriods[column] * 1.0) - mean) / standardDeviation;
                }
                double globalZ = sumTempGlobalZ / trendingSegmentColumns.Count;
                trendingModels[increment] = (trendingModels[increment].item, trendingModels[increment].localZ,globalZ);
            }

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
            summation /= values.Count;
            standardDeviation = Math.Sqrt(summation);
            return standardDeviation;
        }
    }
}
