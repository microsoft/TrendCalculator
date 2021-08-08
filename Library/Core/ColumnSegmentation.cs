using System;
using System.Collections.Generic;
using System.Text;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.AlgoComponents
{
    internal class ColumnSegmentation
    {
        internal List<int> GetHistoricalSegmentColumns(int windowPeriod, int noOfColumns, int numberOfSegmentsOfEachUnit)
        {
            List<int> historicalSegmentColumns = new List<int>();
            int referenceColumn = 0;
            int historicalPeriodUnits = (windowPeriod % 2 == 0) ? windowPeriod / 2 : (windowPeriod / 2) + 1;
            for (int j = 1; j <= historicalPeriodUnits; j++)
            {
                for (int i = 1; i <= numberOfSegmentsOfEachUnit; i++)
                {
                    historicalSegmentColumns.Add(referenceColumn);
                    referenceColumn += 1;
                }
            }
            return historicalSegmentColumns;
        }

        internal List<int> GetTrendingSegmentColumns(int windowPeriod, int noOfColumns, int numberOfSegmentsOfEachUnit)
        {
            List<int> trendingSegmentColumns = new List<int>();
            int noOfTrendingUnits = 0;
            int historicalPeriodUnits = (windowPeriod % 2 == 0) ? windowPeriod / 2 : (windowPeriod / 2) + 1;
            if (windowPeriod * numberOfSegmentsOfEachUnit == noOfColumns)
                noOfTrendingUnits = windowPeriod - historicalPeriodUnits;
            else
                noOfTrendingUnits = windowPeriod - historicalPeriodUnits + 1;
            int referenceColumn = (historicalPeriodUnits * numberOfSegmentsOfEachUnit);
            for (int j = 1; j <= noOfTrendingUnits; j++)
            {
                for (int i = 1; i <= numberOfSegmentsOfEachUnit; i++)
                {
                    if (referenceColumn < noOfColumns)
                    {
                        trendingSegmentColumns.Add(referenceColumn);
                        referenceColumn += 1;
                    }
                }
            }
            return trendingSegmentColumns;
        }
    }
}
