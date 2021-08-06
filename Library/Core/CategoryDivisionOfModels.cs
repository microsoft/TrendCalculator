// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.AlgoComponents
{
    internal class CategoryDivisionOfModels<T> where T : TInterface
    {
        public List<List<(T item, double localZ, double globalZ)>> GetModelsIntoCategory(List<(T item, double localZ, double globalZ)> trendingModels)
        {
            var bothGlobalZLocalZ_Positive_1 = new List<(T item, double localZ, double globalZ)>();
            var bothGlobalZLocalZ_Alternate_2 = new List<(T item, double localZ, double globalZ)>();
            var bothGlobalZLocalZ_Negative_3 = new List<(T item, double localZ, double globalZ)>();

            foreach (var model in trendingModels)
            {
                if (model.globalZ >= 0 && model.localZ >= 0)
                    bothGlobalZLocalZ_Positive_1.Add(model);
                else
                    if ((model.globalZ >= 0 && model.localZ < 0) || (model.globalZ < 0 && model.localZ >= 0))
                    bothGlobalZLocalZ_Alternate_2.Add(model);
                else
                    bothGlobalZLocalZ_Negative_3.Add(model);

            }

            List<List<(T item, double localZ, double globalZ)>> listTrendingModels = new List<List<(T item, double localZ, double globalZ)>>
            {
                bothGlobalZLocalZ_Positive_1,
                bothGlobalZLocalZ_Alternate_2,
                bothGlobalZLocalZ_Negative_3
            };

            return listTrendingModels;
        }
    }
}
