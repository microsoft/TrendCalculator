// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias
{
    internal interface IGlobalZCalculationCriteria
    {
        List<(T item, double localZ, double globalZ)> CalculateGlobalZValue<T>(List<(T item, double localZ, double globalZ)> trendingModels, List<int> historicalSegmentColumns, List<int> trendingSegmentColumns) where T : TInterface;
    }
}