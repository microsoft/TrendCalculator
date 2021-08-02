// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.AlgoComponents
{
    internal class SortingCombiningResults<T> where T : TInterface
    {
        public List<T> GetSortedCombinedResult(List<List<T>> categoryTrendingModels)
        {
            List<T> BothGlobalZLocalZ_Positive_1 = categoryTrendingModels[0];
            List<T> BothGlobalZLocalZ_Alternate_2 = categoryTrendingModels[1];
            List<T> BothGlobalZLocalZ_Negative_3 = categoryTrendingModels[2];
            List<T> trendingModels = new List<T>();

            trendingModels.AddRange(BothGlobalZLocalZ_Positive_1.OrderByDescending(x => x.GlobalZ));
            trendingModels.AddRange(BothGlobalZLocalZ_Alternate_2.OrderByDescending(x => x.LocalZ));
            trendingModels.AddRange(BothGlobalZLocalZ_Negative_3.OrderByDescending(x => x.GlobalZ));

            return trendingModels;
        }

        public List<T> GetSortedCombinedResultV2(List<List<T>> categoryTrendingModels)
        {
            List<T> BothGlobalZLocalZ_Positive_1 = categoryTrendingModels[0];
            List<T> BothGlobalZLocalZ_Alternate_2 = categoryTrendingModels[1];
            List<T> BothGlobalZLocalZ_Negative_3 = categoryTrendingModels[2];
            List<T> trendingModels = new List<T>();

            BothGlobalZLocalZ_Positive_1 = BothGlobalZLocalZ_Positive_1.OrderByDescending(x => x.DemandSupplyQuotient).ToList();
            BothGlobalZLocalZ_Alternate_2 = BothGlobalZLocalZ_Alternate_2.OrderByDescending(x => x.DemandSupplyQuotient).ToList();
            BothGlobalZLocalZ_Negative_3 = BothGlobalZLocalZ_Negative_3.OrderByDescending(x => x.DemandSupplyQuotient).ToList();

            trendingModels.AddRange(BothGlobalZLocalZ_Positive_1.OrderByDescending(x => x.DemandSupplyQuotient));
            trendingModels.AddRange(BothGlobalZLocalZ_Alternate_2.OrderByDescending(x => x.DemandSupplyQuotient));
            trendingModels.AddRange(BothGlobalZLocalZ_Negative_3.OrderByDescending(x => x.DemandSupplyQuotient));

            return trendingModels;
        }

    }
}
