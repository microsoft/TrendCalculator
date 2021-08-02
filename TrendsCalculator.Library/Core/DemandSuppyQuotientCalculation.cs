using System;
using System.Collections.Generic;
using System.Text;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.Core
{
    internal class DemandSuppyQuotientCalculation<T> where T : TInterface
    {
        public List<List<T>> CalculateQuotient(List<List<T>> categoryTrendingModels)
        {
            for (int i = 0; i < categoryTrendingModels.Count; i++)
            {
                for (int j = 0; j < categoryTrendingModels[i].Count; j++)
                {
                    double denominator = 1.0;
                    if (categoryTrendingModels[i][j].SupplyQuantity == 0)
                        denominator = 1.0;
                    else
                        denominator = (double)(categoryTrendingModels[i][j].SupplyQuantity * 1.0);

                    double quotient = (double)(categoryTrendingModels[i][j].GlobalZ / denominator);
                    categoryTrendingModels[i][j].DemandSupplyQuotient = quotient;
                }
            }
            return categoryTrendingModels;
        }
    }
}
