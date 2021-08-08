// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TrendsCalculator.Test")]
namespace TrendsCalculator.Library.Interfaces
{
    public class TModel
    {
        public List<int> CountWithPeriods { get; set; }
    }

    public class TDemandSupplyModel: TModel
    {
        public int SupplyQuantity { get; set; }
        public double DemandSupplyQuotient { get; set; }
    }
}
