using System;
using System.Collections.Generic;
using System.Linq;
using TrendsCalculator.Library.AlgoComponents;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;
using TrendsCalculator.Library.Core;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.TrendingCalculatorForModelsStrategy
{
    internal abstract class AbstractTrendingCalculator
    {
        private bool ValidateInputParams<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> trendingModels) where T : TInterface
        {
            if (trendingModels?.First()?.CountWithPeriods.Count < (windowPeriod * numberOfSegmentsOfEachUnit) &&
                trendingModels?.First()?.CountWithPeriods.Count > ((windowPeriod + 1) * numberOfSegmentsOfEachUnit))
            {
                throw new ArgumentException("Insufficient data as columns in the countWithPeriods attribute of the models");
            }

            return true;
        }

        internal IEnumerable<T> CalculateTrending<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels) where T : TInterface
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
                trendingModels = localZValueCalculation.CalcualteLocalZValue(trendingModels, getHistoricalSegmentColumns, getTrendingSegmentColumns);

                //Calculating Global Z Value
                var globalZValueCalculator = this.GetAlgoConstruct();
                trendingModels = globalZValueCalculator.CalculateGlobalZValue<T>(trendingModels, getHistoricalSegmentColumns, getTrendingSegmentColumns);

                //Dividing The Models Into Three Categories
                CategoryDivisionOfModels<T> categoryDivisionOfModels = new CategoryDivisionOfModels<T>();
                List<List<T>> listOfCategoriesOfTrendingModels = categoryDivisionOfModels.GetModelsIntoCategory(trendingModels);

                //Sorting The Categories And Combining The Result
                SortingCombiningResults<T> sortingCombiningResults = new SortingCombiningResults<T>();
                trendingModels = sortingCombiningResults.GetSortedCombinedResult(listOfCategoriesOfTrendingModels);

                return trendingModels;
            }

            return trendingModels;
        }

        internal IEnumerable<T> CalculateTrendingV2<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels) where T : TInterface
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
                trendingModels = localZValueCalculation.CalcualteLocalZValue(trendingModels, getHistoricalSegmentColumns, getTrendingSegmentColumns);

                //Calculating Global Z Value
                var globalZValueCalculator = this.GetAlgoConstruct();
                trendingModels = globalZValueCalculator.CalculateGlobalZValue<T>(trendingModels, getHistoricalSegmentColumns, getTrendingSegmentColumns);

                //Dividing The Models Into Three Categories
                CategoryDivisionOfModels<T> categoryDivisionOfModels = new CategoryDivisionOfModels<T>();
                List<List<T>> listOfCategoriesOfTrendingModels = categoryDivisionOfModels.GetModelsIntoCategory(trendingModels);

                //Calculation Of Trending Quotient By Dividing With SupplyQuantity
                DemandSuppyQuotientCalculation<T> demandSuppyQuotientCalculation = new DemandSuppyQuotientCalculation<T>();
                List<List<T>> list = demandSuppyQuotientCalculation.CalculateQuotient(listOfCategoriesOfTrendingModels);

                //Sorting The Categories And Combining The Result
                SortingCombiningResults<T> sortingCombiningResults = new SortingCombiningResults<T>();
                trendingModels = sortingCombiningResults.GetSortedCombinedResultV2(list);

                return trendingModels;
            }

            return trendingModels;
        }



        internal abstract IGlobalZCalculationCriteria GetAlgoConstruct();

        internal abstract List<T> PostProcessZScore<T>(List<T> trendingModels) where T : TInterface;
    }
}
