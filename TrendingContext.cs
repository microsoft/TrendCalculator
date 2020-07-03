// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library
{
    internal class TrendingContext<T> where T : TInterface
    {
        private ITrendingStrategy<T> _strategy;

        // Constructor
        public TrendingContext(ITrendingStrategy<T> strategy)
        {
            this._strategy = strategy;
        }

        public IEnumerable<T> ContextInterface(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels)
        {
            return _strategy.CalculateTrendingModels(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels);
        }
    }
}
