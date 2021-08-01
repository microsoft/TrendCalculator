// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias
{
    internal interface IGlobalZCalculationCriteria
    {
        List<T> CalculateGlobalZValue<T>(List<T> trendingModels, List<int> historicalSegmentColumns, List<int> trendingSegmentColumns) where T : TInterface;
    }
}