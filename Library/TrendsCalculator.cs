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

        /// <summary>
        /// This method evaluates the trending data based on the strategy selected
        /// </summary>
        /// <param name="windowPeriod">Window Period to consider for bucketing your input data. E.g. Last 6 months data means window period is 6</param>
        /// <param name="numberOfSegmentsOfEachUnit">Number of segments in each window unit. E.g. each window is divided into 2 buckets</param>
        /// <param name="listOfModels">List of models extending the base class TModel containing the input list of data used for finding trending data</param>
        /// <param name="strategy">Enum of the type TrendCalculationStrategy which returns the trending models as per Custom Criteria or ZMean Criteria</param>
        /// <returns></returns>
        public IEnumerable<T> FindTrendingData<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, List<T> listOfModels, TrendCalculationStrategy strategy) where T: TModel
        {
            var validationMessage = IsInputDataValid(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels.ConvertAll(x => (TModel)x));
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new ArgumentNullException(validationMessage);

            validationMessage = IsCountWithPeriodsDataValid(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels.ConvertAll(x => (TModel)x));
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new ArgumentException(validationMessage);

            this._strategy = strategy;

            AbstractTrendingCalculator baseCalculator = null;
            switch (_strategy)
            {
                case TrendCalculationStrategy.ZMean:
                    baseCalculator = new ZMeanTrendingCalculator();
                    break;
                case TrendCalculationStrategy.Custom:
                    baseCalculator = new CustomTrendingCalculator();
                    break;
                default:
                    baseCalculator = new CustomTrendingCalculator();
                    break;
            }

            var trendingModels = baseCalculator.CalculateTrending<T>(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            var sortedModel = baseCalculator.GetSortedCombinedResult<T>(trendingModels);
            return baseCalculator.PostProcessZScore<T>(sortedModel.ToList()).Select(x => x.item);
        
        }

        /// <summary>
        /// This method evalutes trending data based on demand supply variant
        /// </summary>
        /// <typeparam name="T">T represents generic class, user model which extends the TDemandSupplyModel</typeparam>
        /// <param name="windowPeriod">Window Period to consider for bucketing your input data. E.g. Last 6 months data means window period is 6</param>
        /// <param name="numberOfSegmentsOfEachUnit">Number of segments in each window unit. E.g. each window is divided into 2 buckets</param>
        /// <param name="listOfModels">List of TModel containing the input list of data used for finding trending data</param>
        /// <returns></returns>
        public IEnumerable<T> FindTrendingData<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, List<T> listOfModels) where T: TDemandSupplyModel
        {
            var validationMessage = IsInputDataValid(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels.ConvertAll(x => (TModel)x));
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new ArgumentNullException(validationMessage);

            validationMessage = IsCountWithPeriodsDataValid(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels.ConvertAll(x => (TModel)x));
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new ArgumentException(validationMessage);


                AbstractTrendingCalculator baseCalculator  = new DemandSupplyTrendingCalculator();
            var trendingModels = baseCalculator.CalculateTrending(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            var result = (baseCalculator as DemandSupplyTrendingCalculator).GetSortedCombinedResult(trendingModels);
            var processedModel = new List<(T item, double localZ, double globalZ)>();
            foreach(var model in result)
            {
                processedModel.Add((model.item, model.localZ, model.globalZ));
            }

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

        private string IsCountWithPeriodsDataValid(int windowPeriod,int numberOfSegmentsOfEachUnit, List<TModel> listOfModels)
        {
            string validationMessage = string.Empty;
            int colLength = listOfModels[0].CountWithPeriods.Count;
            foreach (var model in listOfModels)
            {
                if (model.CountWithPeriods.Count != colLength)
                {
                    validationMessage = "All models don't have the same data as columns in CountWithPeriods attribute";
                    break;
                }
            }

            if (!validationMessage.Equals(string.Empty))
                return validationMessage;

            if (listOfModels?.First()?.CountWithPeriods.Count < (windowPeriod * numberOfSegmentsOfEachUnit) &&
                    listOfModels?.First()?.CountWithPeriods.Count > ((windowPeriod + 1) * numberOfSegmentsOfEachUnit))
            {
                validationMessage = "Insufficient data as columns in the countWithPeriods attribute of the models";
            }

            return validationMessage;
        }
    }
}

