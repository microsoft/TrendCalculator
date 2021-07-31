// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace TrendsCalculator.Library.AlgoComponents
{
    internal class HistoricalSegmentColumns
    {
        public List<int> GetHistoricalSegmentColumns(int windowPeriod, int noOfColumns, int numberOfSegmentsOfEachUnit)
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
    }
}


