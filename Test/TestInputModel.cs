using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Test
{
    internal class TestInputModel : TModel
    {
        public string MovieName { get; set; }
    }

    internal class TestDemandSupplyModel : TDemandSupplyModel {
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
                    CountWithPeriods = new List<int>(){ 23, 34, 56, 67, 78, 89 },
                    MovieName = "ABC"

                },
                new TestInputModel()
                {
                    CountWithPeriods = new List<int>(){ 1, 2, 6, 3, 1, 7 },
                    MovieName = "XYZ"
                },
                new TestInputModel()
                {
                    CountWithPeriods = new List<int>(){ 11, 25, 6, 0, 3, 12 },
                    MovieName = "PQR"
                }
            };
        }

        internal static IEnumerable<TestDemandSupplyModel> PrepareDemandData()
        {
            return new List<TestDemandSupplyModel>()
            {
                new TestDemandSupplyModel()
                {
                    SupplyQuantity=1,
                    CountWithPeriods = new List<int>(){ 23, 34, 56, 67, 78, 89 },
                    MovieName = "ABC"

                },
                new TestDemandSupplyModel()
                {
                    SupplyQuantity=4,
                    CountWithPeriods = new List<int>(){ 1, 2, 6, 3, 1, 7 },
                    MovieName = "XYZ"
                },
                new TestDemandSupplyModel()
                {
                    SupplyQuantity=0,
                    CountWithPeriods = new List<int>(){ 11, 25, 6, 0, 3, 12 },
                    MovieName = "PQR"
                }
            };
        }
    }
}
