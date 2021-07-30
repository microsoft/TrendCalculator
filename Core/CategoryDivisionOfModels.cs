// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.AlgoComponents
{
    internal class CategoryDivisionOfModels<T> where T : TInterface
    {
        public List<List<T>> GetModelsIntoCategory(List<T> trendingModels)
        {
            List<T> BothGlobalZLocalZ_Positive_1 = new List<T>();
            List<T> BothGlobalZLocalZ_Alternate_2 = new List<T>();
            List<T> BothGlobalZLocalZ_Negative_3 = new List<T>();

            foreach (T model in trendingModels)
            {
                if (model.GlobalZ >= 0 && model.LocalZ >= 0)
                    BothGlobalZLocalZ_Positive_1.Add(model);
                else
                    if ((model.GlobalZ >= 0 && model.LocalZ < 0) || (model.GlobalZ < 0 && model.LocalZ >= 0))
                    BothGlobalZLocalZ_Alternate_2.Add(model);
                else
                    BothGlobalZLocalZ_Negative_3.Add(model);

            }

            List<List<T>> listTrendingModels = new List<List<T>>
            {
                BothGlobalZLocalZ_Positive_1,
                BothGlobalZLocalZ_Alternate_2,
                BothGlobalZLocalZ_Negative_3
            };

            return listTrendingModels;
        }
    }
}
