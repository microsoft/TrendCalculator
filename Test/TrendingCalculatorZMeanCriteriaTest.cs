using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TrendsCalculator.Library.TrendingCalculatorForModelsStrategy;

namespace TrendsCalculator.Test
{
    [TestClass]
    public class TrendingCalculatorZMeanCriteriaTest
    {
        [TestMethod]
        public void TestTrendingCalculatorZMeanCriteria_ShouldRunSuccessfully_Suite1()
        {
            var trendingStrategy = new ZMeanTrendingCalculator();
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingStrategy.CalculateTrending<TestInputModel>(6, 1, inputData);
            Assert.AreEqual("ABC", output.First().First().item.MovieName);
        }

        [TestMethod]
        public void TestTrendingCalculatorZMeanCriteria_ShouldRunSuccessfully_Suite2()
        {
            var trendingStrategy = new ZMeanTrendingCalculator();
            var inputData = DataPreparator.PrepareData_Suite2();
            var output = trendingStrategy.CalculateTrending<TestInputModel>(6, 1, inputData);
            Assert.AreEqual("ABC", output.First().First().item.MovieName);
            Assert.AreEqual("PQR", output[1][1].item.MovieName);
        }
    }
}
