// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace TrendsCalculator.Library.Interfaces
{
    public interface TInterface
    {
        double LocalZ { get; set; }
        double GlobalZ { get; set; }
        List<int> CountWithPeriods { get; set; }

    }
}
