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
        internal static List<TestInputModel> PrepareData_Suite1()
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

        internal static List<TestInputModel> PrepareData_Suite2()
        {
            return new List<TestInputModel>()
            {
                new TestInputModel()
                {
                    CountWithPeriods = new List<int>(){ 0, 0, 0, 67, 78, 89, 1 },
                    MovieName = "ABC"

                },
                new TestInputModel()
                {
                    CountWithPeriods = new List<int>(){ 0, 0, 0, 0, 0, 0, 0 },
                    MovieName = "XYZ"
                },
                new TestInputModel()
                {
                    CountWithPeriods = new List<int>(){ 11, 25, 6, 0, 3, 12, 10 },
                    MovieName = "PQR"
                }
            };
        }

        internal static List<TestInputModel> PrepareData_Suite3()
        {
            return new List<TestInputModel>()
            {
                new TestInputModel()
                {
                    CountWithPeriods = new List<int>(){ 0, 0, 0, 67 },
                    MovieName = "ABC"

                },
                new TestInputModel()
                {
                    CountWithPeriods = new List<int>(){ 0, 0, 0, 0, 0, 10 },
                    MovieName = "XYZ"
                },
                new TestInputModel()
                {
                    CountWithPeriods = new List<int>(){ 11, 25, 6 },
                    MovieName = "PQR"
                }
            };
        }

        internal static List<TestInputModel> PrepareData_Suite4()
        {
            return new List<TestInputModel>()
            {

            };
        }

        internal static List<TestDemandSupplyModel> PrepareDemandData_Suite1()
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

        internal static List<TestDemandSupplyModel> PrepareDemandData_Suite2()
        {
            return new List<TestDemandSupplyModel>()
            {
                new TestDemandSupplyModel()
                {
                    SupplyQuantity=50,
                    CountWithPeriods = new List<int>(){ 0, 0, 0, 0, 0, 0 },
                    MovieName = "ABC"

                },
                new TestDemandSupplyModel()
                {
                    SupplyQuantity=21,
                    CountWithPeriods = new List<int>(){ 1, 2, 6, 3, 1, 7 },
                    MovieName = "XYZ"
                },
                new TestDemandSupplyModel()
                {
                    SupplyQuantity=13,
                    CountWithPeriods = new List<int>(){ 11, 25, 6, 0, 3, 12 },
                    MovieName = "PQR"
                }
            };
        }

        internal static List<TestDemandSupplyModel> PrepareDemandData_Suite3()
        {
            return new List<TestDemandSupplyModel>()
            {
                new TestDemandSupplyModel()
                {
                    SupplyQuantity=1,
                    CountWithPeriods = new List<int>(){ 0, 0, 0 },
                    MovieName = "ABC"

                },
                new TestDemandSupplyModel()
                {
                    SupplyQuantity=4,
                    CountWithPeriods = new List<int>(){ 1, 2, 6, 3 },
                    MovieName = "XYZ"
                },
                new TestDemandSupplyModel()
                {
                    SupplyQuantity=0,
                    CountWithPeriods = new List<int>(){ 11, 25, 6, 0, 3 },
                    MovieName = "PQR"
                }
            };
        }

        internal static List<TestDemandSupplyModel> PrepareDemandData_Suite4()
        {
            return new List<TestDemandSupplyModel>()
            {
               
            };
        }
    }
}
