// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using TrendsCalculator.Library.AlgoComponents;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.TrendingCalculatorForModelsStrategy
{
    internal class TrendingCalculateForCustomCriteria<T> : ITrendingStrategy<T> where T : TInterface
    {
        public IEnumerable<T> CalculateTrendingModels(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels)
        {

            List<T> trendingModels = new List<T>();
            trendingModels = (List<T>)listOfModels;

            if (trendingModels[0].CountWithPeriods.Count >= (windowPeriod * numberOfSegmentsOfEachUnit) &&
                trendingModels[0].CountWithPeriods.Count <= ((windowPeriod + 1) * numberOfSegmentsOfEachUnit))
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
                GlobalZCalculationCustomCriteria<T> globalZValueCalculation = new GlobalZCalculationCustomCriteria<T>();
                trendingModels = globalZValueCalculation.CalculateGlobalZValue(trendingModels, getHistoricalSegmentColumns, getTrendingSegmentColumns);

                //Dividing The Models Into Three Categories
                CategoryDivisionOfModels<T> categoryDivisionOfModels = new CategoryDivisionOfModels<T>();
                List<List<T>> listOfCategoriesOfTrendingModels = categoryDivisionOfModels.GetModelsIntoCategory(trendingModels);

                //Sorting The Categories And Combining The Result
                SortingCombiningResults<T> sortingCombiningResults = new SortingCombiningResults<T>();
                trendingModels = sortingCombiningResults.GetSortedCombinedResult(listOfCategoriesOfTrendingModels);

                return trendingModels;
            }
            else
            {
                throw new ArgumentException("Insufficient data as columns in the countWithPeriods attribute of the models");
            }
        }
    }
}
