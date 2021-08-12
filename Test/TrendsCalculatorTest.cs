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
        public void TrendsCalculatorTest_CustomCriteria_ShouldRunSuccessfully_Suite1()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData,TrendCalculationStrategy.Custom);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.AreEqual("XYZ", output.ToList()[1].MovieName);
            Assert.AreEqual("PQR", output.ToList()[2].MovieName);
        }

        [TestMethod]
        public void TrendsCalculatorTest_CustomCriteria_ShouldRunSuccessfully_Suite2()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite2();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData, TrendCalculationStrategy.Custom);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.AreEqual("XYZ", output.ToList()[1].MovieName);
            Assert.AreEqual("PQR", output.ToList()[2].MovieName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TrendsCalculatorTest_CustomCriteria_IncorrectData_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite3();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData, TrendCalculationStrategy.Custom);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_BlankRecords_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite4();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData, TrendCalculationStrategy.Custom);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_CustomCriteria_windowPeriodToBeZero_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(0, 1, inputData, TrendCalculationStrategy.Custom);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_CustomCriteria_numberOfSegmentsOfEachUnitToBeZero_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite4();
            var output = trendingCalculator.FindTrendingData(6, 0, inputData, TrendCalculationStrategy.Custom);
        }

        [TestMethod]
        public void TrendsCalculatorTest_ZMeanCriteria_ShouldRunSuccessfully_Suite1()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData, TrendCalculationStrategy.ZMean);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => output.ToList()[1]);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => output.ToList()[2]);
        }

        [TestMethod]
        public void TrendsCalculatorTest_ZMeanCriteria_ShouldRunSuccessfully_Suite2()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite2();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData, TrendCalculationStrategy.ZMean);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => output.ToList()[1]);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => output.ToList()[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TrendsCalculatorTest_ZMeanCriteria_IncorrectData_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite3();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData, TrendCalculationStrategy.ZMean);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_ZMeanCriteria_BlankRecords_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite4();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData, TrendCalculationStrategy.ZMean);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_ZMeanCriteria_windowPeriodToBeZero_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(0, 1, inputData, TrendCalculationStrategy.ZMean);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_ZMeanCriteria_numberOfSegmentsOfEachUnitToBeZero_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareData_Suite1();
            var output = trendingCalculator.FindTrendingData(6, 0, inputData, TrendCalculationStrategy.ZMean);
        }

        [TestMethod]
        public void TrendsCalculatorTest_DemandSupplyCriteria_ShouldRunSuccessfully_Suite1()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareDemandData_Suite1();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);

            Assert.AreEqual("ABC", output.First().MovieName);
            Assert.AreEqual("XYZ", output.ToList()[1].MovieName);
            Assert.AreEqual("PQR", output.ToList()[2].MovieName);
        }

        [TestMethod]
        public void TrendsCalculatorTest_DemandSupplyCriteria_ShouldRunSuccessfully_Suite2()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareDemandData_Suite2();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);

            Assert.AreEqual("XYZ", output.First().MovieName);
            Assert.AreEqual("ABC", output.ToList()[1].MovieName);
            Assert.AreEqual("PQR", output.ToList()[2].MovieName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TrendsCalculatorTest_DemandSupplyCriteria_IncorrectData_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareDemandData_Suite3();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_DemandSupplyCriteria_BlankRecords_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareDemandData_Suite4();
            var output = trendingCalculator.FindTrendingData(6, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_DemandSupplyCriteria_windowPeriodToBeZero_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareDemandData_Suite1();
            var output = trendingCalculator.FindTrendingData(0, 1, inputData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrendsCalculatorTest_DemandSupplyCriteria_numberOfSegmentsOfEachUnitToBeZero_ShouldGiveException()
        {
            var trendingCalculator = new Library.TrendsCalculator();
            var inputData = DataPreparator.PrepareDemandData_Suite1();
            var output = trendingCalculator.FindTrendingData(6, 0, inputData);
        }
    }
}
