// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TrendsCalculator.Test")]
namespace TrendsCalculator.Library.Interfaces
{
    public interface TInterface
    {
        double LocalZ { get; set; }
        double GlobalZ { get; set; }
        int SupplyQuantity { get; set; }
        double DemandSupplyQuotient { get; set; }
        List<int> CountWithPeriods { get; set; }
    }
}
