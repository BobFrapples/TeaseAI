using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TeaseAI.Services.CommandDetection;

namespace TeaseAI.ServicesTests.CommandDetection
{
    [TestClass]
    public class HolidayDetection_Tests
    {
        HolidayDetection _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new HolidayDetection();
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenValentinesDayTagAndNotValentinesDay()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@ValentinesDay", new DateTime(2018, 2, 1)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenValentinesDayTagAndValentinesDay()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@ValentinesDay", new DateTime(2018, 2, 14)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenChristmasEveTagAndNotChristmasEve()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@ChristmasEve", new DateTime(2018, 2, 1)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenChristmasEveTagAndChristmasEve()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@ChristmasEve", new DateTime(2018, 12, 24)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenChristmasDayTagAndNotChristmasDay()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@ChristmasDay", new DateTime(2018, 2, 1)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenChristmasDayTagAndChristmasDay()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@ChristmasDay", new DateTime(2018, 12, 25)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenNewYearsEveTagAndNotNewYearsEve()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@NewYearsEve", new DateTime(2018, 2, 1)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenNewYearsEveTagAndNewYearsEve()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@NewYearsEve", new DateTime(2018, 12, 31)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnFalse_WhenNewYearsDayTagAndNotNewYearsDay()
        {
            Assert.IsFalse(_service.ShouldKeepLine("@NewYearsDay", new DateTime(2018, 2, 1)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenNewYearsDayTagAndNewYearsDay()
        {
            Assert.IsTrue(_service.ShouldKeepLine("@NewYearsDay", new DateTime(2018, 1, 1)));
        }

        [TestMethod]
        public void ShouldKeepLine_ShouldReturnTrue_WhenNoTags()
        {
            Assert.IsTrue(_service.ShouldKeepLine("No tags in this string", new DateTime(2018, 1, 1)));
        }

    }
}
