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
            var trendingCalculator = new TrendsCalculator<TestInputModel>(TrendCalculationStrategy.ZMean);
            var inputData = DataPreparator.PrepareData();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);
            Console.WriteLine("Sorted per ZMean Criteria");
        }

        internal class TestInputModel : TInterface
        {
            public double LocalZ { get; set; }
            public double GlobalZ { get; set; }
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
                    CountWithPeriods = new List<int>(){ 23, 34, 56, 67, 78, 89 },
                    MovieName = "ABC"
                },
                new TestInputModel()
                {
                    CountWithPeriods = new List<int>(){ 1, 2, 6, 3, 1, 7 },
                    MovieName = "XYZ"
                }
            };
            }
        }
    }
}
