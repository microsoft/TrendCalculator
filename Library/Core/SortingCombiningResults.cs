// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using TrendsCalculator.Library.Interfaces;
using TrendsCalculator.Library.Sorter;

namespace TrendsCalculator.Library.AlgoComponents
{
    internal class SortingCombiningResults
    {
        internal List<(T item, double localZ, double globalZ)> GetSortedCombinedResult<T>(List<List<(T item, double localZ, double globalZ)>> categoryTrendingModels) where T : TModel
        {
            var BothGlobalZLocalZ_Positive_1 = categoryTrendingModels[0];
            var BothGlobalZLocalZ_Alternate_2 = categoryTrendingModels[1];
            var BothGlobalZLocalZ_Negative_3 = categoryTrendingModels[2];
            var trendingModels = new List<(T item, double localZ, double globalZ)>();

            trendingModels.AddRange(BothGlobalZLocalZ_Positive_1.OrderByDescending(x => x.globalZ));
            trendingModels.AddRange(BothGlobalZLocalZ_Alternate_2.OrderByDescending(x => x.localZ));
            trendingModels.AddRange(BothGlobalZLocalZ_Negative_3.OrderByDescending(x => x.globalZ));

            return trendingModels;
        }

        internal List<(T item, double localZ, double globalZ)> GetSortedCombinedResultV2<T>(List<List<(T item, double localZ, double globalZ)>> categoryTrendingModels) where T : TDemandSupplyModel
        {
            var bothGlobalZLocalZ_Positive_1 = categoryTrendingModels[0];
            var bothGlobalZLocalZ_Alternate_2 = categoryTrendingModels[1];
            var bothGlobalZLocalZ_Negative_3 = categoryTrendingModels[2];
            var trendingModels = new List<(T item, double localZ, double globalZ)>();

            bothGlobalZLocalZ_Positive_1 = bothGlobalZLocalZ_Positive_1.OrderByDescending(x => x.item.DemandSupplyQuotient).ToList();
            bothGlobalZLocalZ_Alternate_2 = bothGlobalZLocalZ_Alternate_2.OrderByDescending(x => x.item.DemandSupplyQuotient).ToList();
            bothGlobalZLocalZ_Negative_3 = bothGlobalZLocalZ_Negative_3.OrderByDescending(x => x.item.DemandSupplyQuotient).ToList();

            trendingModels.AddRange(bothGlobalZLocalZ_Positive_1.OrderByDescending(x => x.item.DemandSupplyQuotient));
            trendingModels.AddRange(bothGlobalZLocalZ_Alternate_2.OrderByDescending(x => x.item.DemandSupplyQuotient));
            trendingModels.AddRange(bothGlobalZLocalZ_Negative_3.OrderByDescending(x => x.item.DemandSupplyQuotient));

            return trendingModels;
        }
    }
   
}
