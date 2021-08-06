// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;
using TrendsCalculator.Library.Interfaces;
using TrendsCalculator.Library.Sorter;

namespace TrendsCalculator.Library.TrendingCalculatorForModelsStrategy
{
    internal class ZMeanTrendingCalculator : AbstractTrendingCalculator
    {
        internal override IGlobalZCalculationCriteria GetAlgoConstruct()
        {
            return new GlobalZCalculationZMeanCriteria();
        }

        internal override List<(T item, double localZ, double globalZ)> PostProcessZScore<T>(List<(T item, double localZ, double globalZ)> trendingModels)
        {
            var trendingModelsGlobalZMeanCriteria = new List<(T item, double localZ, double globalZ)>();
            double meanGlobalZ = GlobalZCalculationZMeanCriteria.MeanGlobalZ;
            foreach (var model in trendingModels)
            {
                if (model.globalZ >= meanGlobalZ)
                {
                    trendingModelsGlobalZMeanCriteria.Add(model);
                }
            }
            if (trendingModelsGlobalZMeanCriteria.Count > 1)
            {
                var sortingGlobalZ = new GlobalZSorter<T>();
                trendingModelsGlobalZMeanCriteria.Sort(0, trendingModelsGlobalZMeanCriteria.Count, sortingGlobalZ);
            }
            return trendingModelsGlobalZMeanCriteria;
        }
    }
}
