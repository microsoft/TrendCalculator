using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TrendsCalculator.Library.TrendingCalculatorForModelsStrategy;

namespace TrendsCalculator.Test
{
    [TestClass]
    public class TrendingCalculatorCustomCriteriaTest
    {
        [TestMethod]
        public void TestTrendingCalculatorCustomCriteria()
        {
            var trendingStrategy = new CustomTrendingCalculator();
            var inputData = DataPreparator.PrepareData();
            var output = trendingStrategy.CalculateTrending<TestInputModel>(6, 1, inputData);
            Assert.AreEqual("ABC", output.First().item.MovieName);
        }
    }
}
