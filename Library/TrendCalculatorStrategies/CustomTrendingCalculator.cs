// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;

namespace TrendsCalculator.Library.TrendingCalculatorForModelsStrategy
{
    internal class CustomTrendingCalculator : AbstractTrendingCalculator
    {
        internal override IGlobalZCalculationCriteria GetAlgoConstruct()
        {
            return new GlobalZCalculationCustomCriteria();
        }

        internal override List<(T item, double localZ, double globalZ)> PostProcessZScore<T>(List<(T item, double localZ, double globalZ)> trendingModels)
        {
            return trendingModels;
        }
    }
}
