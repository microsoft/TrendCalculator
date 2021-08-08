// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TrendsCalculator.Library.Core.Strategy;
using TrendsCalculator.Library.Helper;
using TrendsCalculator.Library.Interfaces;
using TrendsCalculator.Library.TrendingCalculatorForModelsStrategy;

namespace TrendsCalculator.Library
{
    /// <summary>
    /// This class classifies the data qualifying for trending and return the trending data
    /// </summary>
    /// <typeparam name="T">TInterface to adhere to</typeparam>
    public class TrendsCalculator
    {
        private TrendCalculationStrategy _strategy = TrendCalculationStrategy.ZMean;
        public TrendsCalculator(TrendCalculationStrategy strategy)
        {
            _strategy = strategy;
        }
        
        /// <summary>
        /// This method evaluates the trending data based on the strategy selected
        /// </summary>
        /// <param name="windowPeriod">Window Period to consider for bucketing your input data. E.g. Last 6 months data means window period is 6</param>
        /// <param name="numberOfSegmentsOfEachUnit">Number of segments in each window unit. E.g. each window is divided into 2 buckets</param>
        /// <param name="listOfModels">List of T Model containing the input list of data used for finding trending data</param>
        /// <returns></returns>
        public IEnumerable<T> FindTrendingData<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, List<T> listOfModels) where T: TModel
        {
            var validationMessage = IsInputDataValid(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels as List<TModel>);
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new ArgumentNullException(validationMessage);
            
            AbstractTrendingCalculator baseCalculator = null;
            switch (_strategy)
            {
                case TrendCalculationStrategy.ZMean:
                    baseCalculator = new ZMeanTrendingCalculator();
                    break;
                case TrendCalculationStrategy.Custom:
                    baseCalculator = new CustomTrendingCalculator();
                    break;
                //case TrendCalculationStrategy.DemandSupply:
                //    baseCalculator = new DemandSupplyTrendingCalculator();
                //    break;
                default:
                    baseCalculator = new CustomTrendingCalculator();
                    break;
            }

            var trendingModels = baseCalculator.CalculateTrending<T>(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            var sortedModel = baseCalculator.GetSortedCombinedResult<T>(trendingModels);
            return baseCalculator.PostProcessZScore<T>(sortedModel.ToList()).Select(x => x.item);
        
        }

        public IEnumerable<T> FindTrendingDataOnDemandSupply<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, List<T> listOfModels) where T: TDemandSupplyModel
        {
            var validationMessage = IsInputDataValid(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels.ConvertAll(x => (TModel)x));
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new ArgumentNullException(validationMessage);

            AbstractTrendingCalculator baseCalculator  = new DemandSupplyTrendingCalculator();
            var trendingModels = baseCalculator.CalculateTrending(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            var processedModel = (baseCalculator as DemandSupplyTrendingCalculator).GetSortedCombinedResult(trendingModels);
            return baseCalculator.PostProcessZScore<T>(processedModel).Select(x => x.item);
        }
        

        private string IsInputDataValid(int windowPeriod, int numberOfSegmentsOfEachUnit, List<TModel> listOfModels)
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
}

