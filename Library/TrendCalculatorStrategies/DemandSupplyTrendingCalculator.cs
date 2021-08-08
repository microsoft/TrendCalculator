using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrendsCalculator.Library.AlgoComponents;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;
using TrendsCalculator.Library.Core;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.TrendingCalculatorForModelsStrategy
{
    internal class DemandSupplyTrendingCalculator : AbstractTrendingCalculator
    {
        private int _demandQuotient;
        private double _supplyQuantity;
        
        internal override IGlobalZCalculationCriteria GetAlgoConstruct()
        {
            return new GlobalZCalculationCustomCriteria();
        }
        
        public new List<(T item, double localZ, double globalZ)> GetSortedCombinedResult<T>(List<List<(T item, double localZ, double globalZ)>> categoryTrendingModels) where T : TDemandSupplyModel
        {
            var list = CalculateQuotient<T>(categoryTrendingModels);
            var sortingCombiningResults = new SortingCombiningResults();
            return sortingCombiningResults.GetSortedCombinedResultV2<T>(list);
        }
        private List<List<(Y item, double localZ, double globalZ)>> CalculateQuotient<Y>(List<List<(Y item, double localZ, double globalZ)>> categoryTrendingModels) where Y: TDemandSupplyModel
        {
            for (int i = 0; i < categoryTrendingModels.Count; i++)
            {
                for (int j = 0; j < categoryTrendingModels[i].Count; j++)
                {
                    double denominator = 1.0;
                    if (categoryTrendingModels[i][j].item.SupplyQuantity == 0)
                        denominator = 1.0;
                    else
                        denominator = (double)(categoryTrendingModels[i][j].item.SupplyQuantity * 1.0);

                    double quotient = (double)(categoryTrendingModels[i][j].globalZ / denominator);
                    categoryTrendingModels[i][j].item.DemandSupplyQuotient = quotient;
                }
            }

            return categoryTrendingModels;
        }

        internal override List<(T item, double localZ, double globalZ)> PostProcessZScore<T>(List<(T item, double localZ, double globalZ)> trendingModels)
        {
            return trendingModels;
        }
    }
}
