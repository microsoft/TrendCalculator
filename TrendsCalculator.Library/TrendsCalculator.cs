// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        private static TInternal TransformTInterfaceToTInternal(T input)
        {
            foreach (PropertyInfo prop in input.GetType().GetProperties())
                TInternal.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(input, null), null);
            return (dynamic)input;
            //TInternal output = new Internal()
            //{
            //    CountWithPeriods = input.CountWithPeriods,
            //    GlobalZ = 0,
            //    LocalZ = 0
            //};
            //return output;
        }


        /// <summary>
        /// This method evaluates the trending data based on the strategy selected
        /// </summary>
        /// <param name="windowPeriod">Window Period to consider for bucketing your input data. E.g. Last 6 months data means window period is 6</param>
        /// <param name="numberOfSegmentsOfEachUnit">Number of segments in each window unit. E.g. each window is divided into 2 buckets</param>
        /// <param name="listOfModels">List of T Model containing the input list of data used for finding trending data</param>
        /// <returns></returns>
        public IEnumerable<T> FindTrendingData(int windowPeriod, int numberOfSegmentsOfEachUnit, List<T> listOfModels)
        {
            var validationMessage = IsInputDataValid(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
            if (!string.IsNullOrWhiteSpace(validationMessage))
                throw new ArgumentNullException(validationMessage);


            var transformedModel = listOfModels.ConvertAll(new Converter<T, TInternal>(TransformTInterfaceToTInternal));

            AbstractTrendingCalculator baseCalculator = null;
            switch (_strategy)
            {
                case TrendCalculationStrategy.ZMean:
                    baseCalculator = new ZMeanTrendingCalculator();
                    break;
                case TrendCalculationStrategy.Custom:
                default:
                    baseCalculator = new CustomTrendingCalculator();
                    break;
            }

            var trendingModels = baseCalculator.CalculateTrending<TInternal>(windowPeriod, numberOfSegmentsOfEachUnit, transformedModel);
            return baseCalculator.PostProcessZScore<TInternal>(trendingModels?.ToList()) as List<T>;
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
}

