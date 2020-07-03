// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;
using TrendsCalculator.Library.Interfaces;
using TrendsCalculator.Library.TrendingCalculatorForModelsStrategy;

namespace TrendsCalculator.Library
{
    /// <summary>
    /// This enum enabled to set the trend calculation strategy 
    /// </summary>
    public enum TrendCalculationStrategy
    {
        /// <summary>
        /// Z Mean Strategy will consider mean of all the Z values and return trend results according to mean Z threshold
        /// </summary>
        ZMean,
        /// <summary>
        /// Custom strategy will return the Z mean but won't filter the results based on any threshold.
        /// </summary>
        Custom
    }

    /// <summary>
    /// This class classifies the data qualifying for trending and return the trending data
    /// </summary>
    /// <typeparam name="T">TInterface to adhere to</typeparam>
    public class TrendingDataCalculator<T> where T : TInterface
    {
        private TrendCalculationStrategy _strategy;
        public TrendingDataCalculator(TrendCalculationStrategy strategy)
        {
            _strategy = strategy;
        }

        /// <summary>
        /// This method evaluates the trending data based on the strategy selected
        /// </summary>
        /// <param name="windowPeriod">Window Period to consider for bucketing</param>
        /// <param name="numberOfSegmentsOfEachUnit">Number of segments in each bucket</param>
        /// <param name="listOfModels">List of raw data to evaluate for trend</param>
        /// <returns></returns>
        public List<T> EvalAndGetTrendingData(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels)
        {
            switch (_strategy) 
            {
                case TrendCalculationStrategy.ZMean:
                    return this.GetTrendingModelsZMeanCriteria(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
                case TrendCalculationStrategy.Custom:
                    return this.GetTrendingModelsCustomCriteria(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
                default:
                    return this.GetTrendingModelsCustomCriteria(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            }

        }
        private List<T> GetTrendingModelsZMeanCriteria(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels)
        {
            TrendingContext<T> trendingContext = new TrendingContext<T>(new TrendingCalculateForZMeanCriteria<T>());
            List<T> trendingModels = (List<T>)trendingContext.ContextInterface(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            List<T> trendingModelsGlobalZMeanCriteria = new List<T>();
            double meanGlobalZ = GlobalZCalculationZMeanCriteria<T>.meanGlobalZ;
            foreach (T model in trendingModels)
            {
                if (model.GlobalZ >= meanGlobalZ)
                {
                    trendingModelsGlobalZMeanCriteria.Add(model);
                }
            }
            if (trendingModelsGlobalZMeanCriteria.Count > 1)
            {
                SortingGLobalZ<T> sortingGLobalZ = new SortingGLobalZ<T>();
                trendingModelsGlobalZMeanCriteria.Sort(0, trendingModelsGlobalZMeanCriteria.Count, sortingGLobalZ);
            }
            return trendingModelsGlobalZMeanCriteria;
        }
        private  List<T> GetTrendingModelsCustomCriteria(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels)
        {
            TrendingContext<T> trendingContext = new TrendingContext<T>(new TrendingCalculateForCustomCriteria<T>());
            List<T> trendingModels = (List<T>)trendingContext.ContextInterface(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            return trendingModels;
        }
    }

    internal class SortingGLobalZ<T> : IComparer<T> where T : TInterface
    {
        public int Compare(T x, T y)
        {
            return (x.GlobalZ <= y.GlobalZ) ? 1 : -1;
        }
    }
}

