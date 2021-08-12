using System;
using System.Collections.Generic;
using TrendsCalculator.Library;
using TrendsCalculator.Library.Core.Strategy;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var trendingCalculator = new TrendsCalculator.Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);
            Console.WriteLine("Sorted per Demand Supply criteria");
        }

        internal class TestInputModel : TDemandSupplyModel
        {
            public string MovieName { get; set; }
        }

        internal static class DataPreparator
        {
            internal static List<TestInputModel> PrepareData()
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
}
