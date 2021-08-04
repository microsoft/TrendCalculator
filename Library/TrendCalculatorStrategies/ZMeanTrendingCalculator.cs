// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;
using TrendsCalculator.Library.Sorter;

namespace TrendsCalculator.Library.TrendingCalculatorForModelsStrategy
{
    internal class ZMeanTrendingCalculator : AbstractTrendingCalculator
    {
        internal override IGlobalZCalculationCriteria GetAlgoConstruct()
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
                GlobalZSorter<T> sortingGLobalZ = new GlobalZSorter<T>();
                trendingModelsGlobalZMeanCriteria.Sort(0, trendingModelsGlobalZMeanCriteria.Count, sortingGLobalZ);
            }
            return trendingModelsGlobalZMeanCriteria;
        }
    }
}
