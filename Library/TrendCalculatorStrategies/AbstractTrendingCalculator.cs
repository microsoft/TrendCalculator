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
        protected bool ValidateInputParams<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> trendingModels) where T : TModel 
        {
            if (trendingModels?.First()?.CountWithPeriods.Count < (windowPeriod * numberOfSegmentsOfEachUnit) &&
                trendingModels?.First()?.CountWithPeriods.Count > ((windowPeriod + 1) * numberOfSegmentsOfEachUnit))
            {
                throw new ArgumentException("Insufficient data as columns in the countWithPeriods attribute of the models");
            }

            return true;
        }

        public virtual List<List<(T item, double localZ, double globalZ)>> CalculateTrending<T>(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels) where T : TModel
        {
            var trendingModels = new List<T>();
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
                return categoryDivisionOfModels.GetModelsIntoCategory(calculatedTrendingModels);
            }

            return null;
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
