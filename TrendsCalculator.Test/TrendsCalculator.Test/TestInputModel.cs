using System.Collections.Generic;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Test
{
    internal class TestInputModel : TInternal
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
