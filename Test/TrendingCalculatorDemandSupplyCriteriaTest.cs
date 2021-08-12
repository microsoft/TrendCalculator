using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrendsCalculator.Library.TrendingCalculatorForModelsStrategy;

namespace TrendsCalculator.Test
{
    [TestClass]
    public class TrendingCalculatorDemandSupplyCriteriaTest
    {
        [TestMethod]
        public void TestTrendingCalculatorDemandSupplyCriteria_ShouldRunSuccessfully_Suite1()
        {
            var trendingStrategy = new DemandSupplyTrendingCalculator();
            var inputData = DataPreparator.PrepareDemandData_Suite1();
            var output = trendingStrategy.CalculateTrending(6, 1, inputData);
            Assert.AreEqual("ABC", output.First().First().item.MovieName);
            Assert.AreEqual("PQR", output.Last().First().item.MovieName);
        }

        [TestMethod]
        public void TestTrendingCalculatorDemandSupplyCriteria_ShouldRunSuccessfully_Suite2()
        {
            var trendingStrategy = new DemandSupplyTrendingCalculator();
            var inputData = DataPreparator.PrepareDemandData_Suite2();
            var output = trendingStrategy.CalculateTrending(6, 1, inputData);
            Assert.AreEqual("ABC", output[1].First().item.MovieName);
            Assert.AreEqual("PQR", output.Last().First().item.MovieName);
        }
    }
}
