// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace TrendsCalculator.Library.Interfaces
{
    internal interface ITrendingStrategy<T> where T : TInterface
    {
        IEnumerable<T> CalculateTrendingModels(int windowPeriod, int numberOfSegmentsOfEachUnit, IEnumerable<T> listOfModels);
    }
}
