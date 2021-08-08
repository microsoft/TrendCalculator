using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrendsCalculator.Library.Core.Strategy;

namespace TrendsCalculator.Test
{
    [TestClass]
    public class TrendsCalculatorTest
    {
        [TestMethod]
        public void TrendsCalculatorTest_CustomCriteria_Suite1()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.Custom);
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.AreEqual("XYZ", output.ToList()[1].MovieName);
            Assert.AreEqual("PQR", output.ToList()[2].MovieName);
        }

        [TestMethod]
        public void TrendsCalculatorTest_CustomCriteria_Suite2()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.Custom);
            var inputData = DataPreparator.PrepareData_Suite2();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.AreEqual("XYZ", output.ToList()[1].MovieName);
            Assert.AreEqual("PQR", output.ToList()[2].MovieName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TrendsCalculatorTest_CustomCriteria_Suite3()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.Custom);
            var inputData = DataPreparator.PrepareData_Suite3();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_CustomCriteria_Suite4()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.Custom);
            var inputData = DataPreparator.PrepareData_Suite4();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_CustomCriteria_windowPeriodToBeZero()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.Custom);
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(0, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_CustomCriteria_numberOfSegmentsOfEachUnitToBeZero()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.Custom);
            var inputData = DataPreparator.PrepareData_Suite4();
            var output = trendingCalculator.FindTrendingData(6, 0, inputData);
        }

        [TestMethod]
        public void TrendsCalculatorTest_ZMeanCriteria_Suite1()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.ZMean);
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => output.ToList()[1]);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => output.ToList()[2]);
        }

        [TestMethod]
        public void TrendsCalculatorTest_ZMeanCriteria_Suite2()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.ZMean);
            var inputData = DataPreparator.PrepareData_Suite2();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => output.ToList()[1]);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => output.ToList()[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TrendsCalculatorTest_ZMeanCriteria_Suite3()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.ZMean);
            var inputData = DataPreparator.PrepareData_Suite3();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_ZMeanCriteria_Suite4()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.ZMean);
            var inputData = DataPreparator.PrepareData_Suite4();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_ZMeanCriteria_windowPeriodToBeZero()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.ZMean);
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(0, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_ZMeanCriteria_numberOfSegmentsOfEachUnitToBeZero()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.ZMean);
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(6, 0, inputData);
        }

        [TestMethod]
        public void TrendsCalculatorTest_DemandSupplyCriteria_Suite1()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.DemandSupply);
            var inputData = DataPreparator.PrepareDemandData_Suite1();
            var output = trendingCalculator.FindTrendingDataOnDemandSupply(6, 1, inputData);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.AreEqual("XYZ", output.ToList()[1].MovieName);
            Assert.AreEqual("PQR", output.ToList()[2].MovieName);
        }

        [TestMethod]
        public void TrendsCalculatorTest_DemandSupplyCriteria_Suite2()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.DemandSupply);
            var inputData = DataPreparator.PrepareDemandData_Suite2();
            var output = trendingCalculator.FindTrendingDataOnDemandSupply(6, 1, inputData);

            Assert.AreEqual("XYZ", output.First().MovieName);
            Assert.AreEqual("ABC", output.ToList()[1].MovieName);
            Assert.AreEqual("PQR", output.ToList()[2].MovieName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TrendsCalculatorTest_DemandSupplyCriteria_Suite3()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.DemandSupply);
            var inputData = DataPreparator.PrepareDemandData_Suite3();
            var output = trendingCalculator.FindTrendingDataOnDemandSupply(6, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_DemandSupplyCriteria_Suite4()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.DemandSupply);
            var inputData = DataPreparator.PrepareDemandData_Suite4();
            var output = trendingCalculator.FindTrendingDataOnDemandSupply(6, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_DemandSupplyCriteria_windowPeriodToBeZero()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.DemandSupply);
            var inputData = DataPreparator.PrepareDemandData_Suite1();
            var output = trendingCalculator.FindTrendingDataOnDemandSupply(0, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_DemandSupplyCriteria_numberOfSegmentsOfEachUnitToBeZero()
        {
            var trendingCalculator = new Library.TrendsCalculator(TrendCalculationStrategy.DemandSupply);
            var inputData = DataPreparator.PrepareDemandData_Suite1();
            var output = trendingCalculator.FindTrendingDataOnDemandSupply(6, 0, inputData);
        }
    }
}
