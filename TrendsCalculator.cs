// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;
using TrendsCalculator.Library.Core.Strategy;
using TrendsCalculator.Library.Interfaces;
using TrendsCalculator.Library.TrendingCalculatorForModelsStrategy;

namespace TrendsCalculator.Library
{
    /// <summary>
    /// This class classifies the data qualifying for trending and return the trending data
    /// </summary>
    /// <typeparam name="T">TInterface to adhere to</typeparam>
    public class TrendsCalculator<T> where T : TInterface
    {
        private TrendCalculationStrategy _strategy = TrendCalculationStrategy.ZMean;
        public TrendsCalculator(TrendCalculationStrategy strategy)
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
            var validationMessage = IsInputDataValid(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new ArgumentNullException(validationMessage);

            switch (_strategy) 
            {
                case TrendCalculationStrategy.ZMean:
                    return this.GetTrendingModelsZMeanCriteria(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
                case TrendCalculationStrategy.Custom:
                default:
                    return this.GetTrendingModelsCustomCriteria(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            }
        }

        private string IsInputDataValid(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels)
        {
            string validationMessage = string.Empty;
            if (windowPeriod == 0)
                validationMessage = "windowPeriod cannot be zero";
            else if (numberOfSegmentsOfEachUnit == 0)
                validationMessage = "numberOfSegmentsOfEachUnit cannot be zero";
            else if (listOfModels == null || listOfModels.Count() == 0)
                validationMessage = "listOfModels can't have zero records";

            return validationMessage;
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

