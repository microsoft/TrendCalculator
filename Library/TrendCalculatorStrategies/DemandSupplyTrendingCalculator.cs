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
        
        public new List<(T item, double localZ, double globalZ, double DemandSupplyQuotient)> GetSortedCombinedResult<T>(List<List<(T item, double localZ, double globalZ)>> categoryTrendingModels) where T : TDemandSupplyModel
        {
            var list = CalculateQuotient<T>(categoryTrendingModels);
            var sortingCombiningResults = new SortingCombiningResults();
            return sortingCombiningResults.GetSortedCombinedResultV2<T>(list);
        }
        private List<List<(Y item, double localZ, double globalZ,double DemandSupplyQuotient)>> CalculateQuotient<Y>(List<List<(Y item, double localZ, double globalZ)>> categoryTrendingModels) where Y: TDemandSupplyModel
        {
            var categoryDemandSupplyTrendingModels = new List<List<(Y item, double localZ, double globalZ, double DemandSupplyQuotient)>>();

            for (int i = 0; i < categoryTrendingModels.Count; i++)
            {
                var transformedModelCategory = new List<(Y item, double localZ, double globalZ, double DemandSupplyQuotient)>();
                for (int j = 0; j < categoryTrendingModels[i].Count; j++)
                {
                    double denominator;
                    if (categoryTrendingModels[i][j].item.SupplyQuantity == 0)
                        denominator = 1.0;
                    else
                        denominator = (double)(categoryTrendingModels[i][j].item.SupplyQuantity * 1.0);

                    double quotient = (double)(categoryTrendingModels[i][j].globalZ / denominator);
                    transformedModelCategory.Add((categoryTrendingModels[i][j].item,
                                                categoryTrendingModels[i][j].localZ,
                                                categoryTrendingModels[i][j].globalZ, 
                                                quotient));
                }
                categoryDemandSupplyTrendingModels.Add(transformedModelCategory);
            }

            return categoryDemandSupplyTrendingModels;
        }

        internal override List<(T item, double localZ, double globalZ)> PostProcessZScore<T>(List<(T item, double localZ, double globalZ)> trendingModels)
        {
            return trendingModels;
        }
    }
}
