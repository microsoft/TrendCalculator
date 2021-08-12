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

        internal List<(T item, double localZ, double globalZ,double DemandSupplyQuotient)> GetSortedCombinedResultV2<T>(List<List<(T item, double localZ, double globalZ, double DemandSupplyQuotient)>> categoryDemandSupplyTrendingModels) where T : TDemandSupplyModel
        {
            var bothGlobalZLocalZ_Positive_1 = categoryDemandSupplyTrendingModels[0];
            var bothGlobalZLocalZ_Alternate_2 = categoryDemandSupplyTrendingModels[1];
            var bothGlobalZLocalZ_Negative_3 = categoryDemandSupplyTrendingModels[2];
            var trendingModels = new List<(T item, double localZ, double globalZ, double DemandSupplyQuotient)>();

            trendingModels.AddRange(bothGlobalZLocalZ_Positive_1.OrderByDescending(x => x.DemandSupplyQuotient));
            trendingModels.AddRange(bothGlobalZLocalZ_Alternate_2.OrderByDescending(x => x.DemandSupplyQuotient));
            trendingModels.AddRange(bothGlobalZLocalZ_Negative_3.OrderByDescending(x => x.DemandSupplyQuotient));

            return trendingModels;
        }
    }
   
}
