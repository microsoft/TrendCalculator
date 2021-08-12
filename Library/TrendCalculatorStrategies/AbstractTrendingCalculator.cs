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
        public virtual List<List<(T item, double localZ, double globalZ)>> CalculateTrending<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels) where T : TModel
        {
            var trendingModels = new List<T>();
            trendingModels = (List<T>)listOfModels;
                int noOfColumns = trendingModels[0].CountWithPeriods.Count;

                // LOGIC FOR COMPONENTIZING THE TRENDING SKILL
                //DIVIDE THE WINDOW INTO HISTORY SEGMENT AND TRENDING SEGMENT

                ColumnSegmentation getColumnSegments = new ColumnSegmentation();
                List<int> getHistoricalSegmentColumns = getColumnSegments.GetHistoricalSegmentColumns(windowPeriod, noOfColumns, numberOfSegmentsOfEachUnit);
                List<int> getTrendingSegmentColumns = getColumnSegments.GetTrendingSegmentColumns(windowPeriod, noOfColumns, numberOfSegmentsOfEachUnit);

                //Calculating Local Z Value
                LocalZValueCalculation<T> localZValueCalculation = new LocalZValueCalculation<T>();
                var calculatedTrendingModels = localZValueCalculation.CalculateLocalZValue(trendingModels, getHistoricalSegmentColumns, getTrendingSegmentColumns);

                //Calculating Global Z Value
                var globalZValueCalculator = this.GetAlgoConstruct();
                calculatedTrendingModels = globalZValueCalculator.CalculateGlobalZValue<T>(calculatedTrendingModels, getHistoricalSegmentColumns, getTrendingSegmentColumns);

                //Dividing The Models Into Three Categories
                CategoryDivisionOfModels<T> categoryDivisionOfModels = new CategoryDivisionOfModels<T>();
                return categoryDivisionOfModels.GetModelsIntoCategory(calculatedTrendingModels);
        }

        public List<(T item, double localZ, double globalZ)> GetSortedCombinedResult<T>(List<List<(T item, double localZ, double globalZ)>> categoryTrendingModels) where T : TModel
        {
            SortingCombiningResults sortingCombiningResults = new SortingCombiningResults();
            return sortingCombiningResults.GetSortedCombinedResult<T>(categoryTrendingModels);
        }

        internal abstract IGlobalZCalculationCriteria GetAlgoConstruct();

        internal abstract List<(T item, double localZ, double globalZ)> PostProcessZScore<T>(List<(T item, double localZ, double globalZ)> trendingModels) where T : TModel;
    }
}
