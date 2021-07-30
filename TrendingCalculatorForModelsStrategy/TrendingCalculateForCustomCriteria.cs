// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using TrendsCalculator.Library.AlgoComponents;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.TrendingCalculatorForModelsStrategy
{
    internal class TrendingCalculateForCustomCriteria : BaseTrendingCalculator
    {
        internal override IAlgoCriteria GetAlgoConstruct()
        {
            return new GlobalZCalculationCustomCriteria();
        }

        internal override List<T> PostProcessZScore<T>(List<T> trendingModels)
        {
            return trendingModels;
        }
    }
}
