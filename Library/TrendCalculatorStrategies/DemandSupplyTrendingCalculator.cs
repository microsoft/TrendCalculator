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
        internal override IGlobalZCalculationCriteria GetAlgoConstruct()
        {
            return new GlobalZCalculationCustomCriteria();
        }

        internal override IEnumerable<(T item, double localZ, double globalZ)> CalculateTrending<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels)
        {
            List<T> trendingModels = new List<T>();
            trendingModels = (List<T>)listOfModels;
            if (ValidateInputParams<T>(windowPeriod, numberOfSegmentsOfEachUnit, listOfModels))
            {
                int noOfColumns = trendingModels[0].CountWithPeriods.Count;

                // LOGIC FOR COMPONENTIZING THE TRENDING SKILL
                //DIVIDE THE WINDOW INTO HISTORY SEGMENT AND TRENDING SEGMENT
                HistoricalSegmentColumns historicalSegmentColumns = new HistoricalSegmentColumns();
                List<int> getHistoricalSegmentColumns = historicalSegmentColumns.GetHistoricalSegmentColumns(windowPeriod, noOfColumns, numberOfSegmentsOfEachUnit);

                TrendingSegmentColumns trendingSegmentColumns = new TrendingSegmentColumns();
                List<int> getTrendingSegmentColumns = trendingSegmentColumns.GetTrendingSegmentColumns(windowPeriod, noOfColumns, numberOfSegmentsOfEachUnit);

                //Calculating Local Z Value
                LocalZValueCalculation<T> localZValueCalculation = new LocalZValueCalculation<T>();
                var calculatedTrendingModels = localZValueCalculation.CalculateLocalZValue(trendingModels, getHistoricalSegmentColumns, getTrendingSegmentColumns);

                //Calculating Global Z Value
                var globalZValueCalculator = this.GetAlgoConstruct();
                calculatedTrendingModels = globalZValueCalculator.CalculateGlobalZValue<T>(calculatedTrendingModels, getHistoricalSegmentColumns, getTrendingSegmentColumns);

                //Dividing The Models Into Three Categories
                CategoryDivisionOfModels<T> categoryDivisionOfModels = new CategoryDivisionOfModels<T>();
                var listOfCategoriesOfTrendingModels = categoryDivisionOfModels.GetModelsIntoCategory(calculatedTrendingModels);

                //Calculation Of Trending Quotient By Dividing With SupplyQuantity
                var list = CalculateQuotient(listOfCategoriesOfTrendingModels);

                //Sorting The Categories And Combining The Result
                SortingCombiningResults<T> sortingCombiningResults = new SortingCombiningResults<T>();
                return sortingCombiningResults.GetSortedCombinedResultV2(list);
            }
            return null;
        }

        private List<List<(T item, double localZ, double globalZ)>> CalculateQuotient<T>(List<List<(T item, double localZ, double globalZ)>> categoryTrendingModels) where T: TInterface
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
