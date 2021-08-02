using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Test
{
    internal class TestInputModel : TInterface
    {
        public double LocalZ { get; set; }
        public double GlobalZ { get; set; }
        public int SupplyQuantity { get; set; }
        public double DemandSupplyQuotient { get; set; }
        public List<int> CountWithPeriods { get; set; }

        public string MovieName { get; set; }
    }

    internal static class DataPreparator
    {
        internal static IEnumerable<TestInputModel> PrepareData()
        {
            return new List<TestInputModel>()
            {
                new TestInputModel()
                {
                    SupplyQuantity=1,
                    CountWithPeriods = new List<int>(){ 23, 34, 56, 67, 78, 89 },
                    MovieName = "ABC"

                },
                new TestInputModel()
                {
                    SupplyQuantity=4,
                    CountWithPeriods = new List<int>(){ 1, 2, 6, 3, 1, 7 },
                    MovieName = "XYZ"
                },
                new TestInputModel()
                {
                    SupplyQuantity=0,
                    CountWithPeriods = new List<int>(){ 11, 25, 6, 0, 3, 12 },
                    MovieName = "PQR"
                }
            };
        }
    }
}
