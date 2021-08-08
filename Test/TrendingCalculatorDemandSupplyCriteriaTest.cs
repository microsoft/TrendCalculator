﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void TestTrendingCalculatorDemandSupplyCriteria()
        {
            var trendingStrategy = new DemandSupplyTrendingCalculator();
            var inputData = DataPreparator.PrepareData();
            var output = trendingStrategy.CalculateTrending<TestInputModel>(6, 1, inputData);
            Assert.AreEqual("ABC", output.First().First().item.MovieName);
            Assert.AreEqual("PQR", output.Last().First().item.MovieName);
        }
    }
}
