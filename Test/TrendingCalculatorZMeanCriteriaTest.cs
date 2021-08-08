using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TrendsCalculator.Library.TrendingCalculatorForModelsStrategy;

namespace TrendsCalculator.Test
{
    [TestClass]
    public class TrendingCalculatorZMeanCriteriaTest
    {
        [TestMethod]
        public void TestTrendingCalculatorZMeanCriteria()
        {
            var trendingStrategy = new ZMeanTrendingCalculator();
            var inputData = DataPreparator.PrepareData();
            var output = trendingStrategy.CalculateTrending<TestInputModel>(6, 1, inputData);
            Assert.AreEqual("ABC", output.First().First().item.MovieName);
        }
    }
}
