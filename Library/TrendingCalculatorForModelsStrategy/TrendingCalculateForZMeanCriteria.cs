// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;

namespace TrendsCalculator.Library.TrendingCalculatorForModelsStrategy
{
    internal class TrendingCalculateForZMeanCriteria : BaseTrendingCalculator
    {
        internal override IAlgoCriteria GetAlgoConstruct()
        {
            return new GlobalZCalculationZMeanCriteria();
        }

        internal override List<T> PostProcessZScore<T>(List<T> trendingModels)
        {
            var trendingModelsGlobalZMeanCriteria = new List<T>();
            double meanGlobalZ = GlobalZCalculationZMeanCriteria.MeanGlobalZ;
            foreach (T model in trendingModels)
            {
                if (model.GlobalZ >= meanGlobalZ)
                {
                    trendingModelsGlobalZMeanCriteria.Add(model);
                }
            }
            if (trendingModelsGlobalZMeanCriteria.Count > 1)
            {
                SortingGlobalZ<T> sortingGLobalZ = new SortingGlobalZ<T>();
                trendingModelsGlobalZMeanCriteria.Sort(0, trendingModelsGlobalZMeanCriteria.Count, sortingGLobalZ);
            }
            return trendingModelsGlobalZMeanCriteria;
        }
    }
}
