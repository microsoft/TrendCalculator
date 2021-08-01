// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
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

            SortingBothPositiveNegative<T> sortingBothPositiveNegative = new SortingBothPositiveNegative<T>();
            SortingAlternate<T> sortingAlternate = new SortingAlternate<T>();

            if (BothGlobalZLocalZ_Positive_1.Count - 1 > 0)
                BothGlobalZLocalZ_Positive_1.Sort(0, BothGlobalZLocalZ_Positive_1.Count, sortingBothPositiveNegative);

            if (BothGlobalZLocalZ_Alternate_2.Count - 1 > 0)
                BothGlobalZLocalZ_Alternate_2.Sort(0, BothGlobalZLocalZ_Alternate_2.Count, sortingAlternate);

            if (BothGlobalZLocalZ_Negative_3.Count - 1 > 0)
                BothGlobalZLocalZ_Negative_3.Sort(0, BothGlobalZLocalZ_Negative_3.Count, sortingBothPositiveNegative);

            foreach (T model in BothGlobalZLocalZ_Positive_1)
                trendingModels.Add(model);

            foreach (T model in BothGlobalZLocalZ_Alternate_2)
                trendingModels.Add(model);

            foreach (T model in BothGlobalZLocalZ_Negative_3)
                trendingModels.Add(model);

            return trendingModels;
        }
    }
    internal class SortingBothPositiveNegative<T> : IComparer<T> where T : TInterface
    {
        public int Compare(T x, T y)
        {
            return (x.GlobalZ <= y.GlobalZ) ? 1 : -1;
        }
    }
    internal class SortingAlternate<T> : IComparer<T> where T : TInterface
    {
        public int Compare(T x, T y)
        {
            return (x.LocalZ <= y.LocalZ) ? 1 : -1;
        }
    }
}
