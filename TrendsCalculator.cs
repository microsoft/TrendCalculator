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
        public IEnumerable<T> FindTrendingData(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels)
        {
            var validationMessage = IsInputDataValid(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new ArgumentNullException(validationMessage);

            BaseTrendingCalculator baseCalculator = null;
            switch (_strategy) 
            {
                case TrendCalculationStrategy.ZMean:
                    baseCalculator = new TrendingCalculateForZMeanCriteria();
                    break;
                case TrendCalculationStrategy.Custom:
                default:
                    baseCalculator = new TrendingCalculateForCustomCriteria();
                    break;
            }

            var trendingModels = baseCalculator.CalculateTrending<T>(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            return baseCalculator.PostProcessZScore<T>(trendingModels?.ToList());
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
    }

    internal class SortingGlobalZ<T> : IComparer<T> where T : TInterface
    {
        public int Compare(T x, T y)
        {
            return (x.GlobalZ <= y.GlobalZ) ? 1 : -1;
        }
    }
}

