using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using Swapi.Core.Calculation;
using Swapi.Core.Calculation.Strategy;
using Swapi.Core.Model;
using Xunit;

namespace Swapi.Core.UnitTests.Calculation.Strategy
{
    public class StopsCalculatorTests
    {
        [Fact]
        public void CalculateTotalStops_WhenRangeCalculationNull_ReturnsInvalidResult()
        {
            var moqFactory = new Mock<IRangeCalculationStrategyFactory>();
            moqFactory.Setup(mf => mf.GetRangeCalculationStrategy(It.IsAny<IntervalUnit>()))
                .Returns(CreateMoqStrategy(null).Object);

            var testCalculator = new StopsCalculator(moqFactory.Object,new Mock<ILogger>().Object);
            var result = testCalculator.CalculateTotalStops(StarshipUtils.Create(IntervalUnit.Unknown, 1, 1), 1);
            Assert.False(result.IsValid);
            Assert.Null(result.NumberOfStops);
        }

        [Fact]
        public void CalculateTotalStops_WhenRangeCalculationNotNull_ReturnsValidResult()
        {
            var moqFactory = new Mock<IRangeCalculationStrategyFactory>();
            moqFactory.Setup(mf => mf.GetRangeCalculationStrategy(It.IsAny<IntervalUnit>()))
                .Returns(CreateMoqStrategy(2).Object);

            var testCalculator = new StopsCalculator(moqFactory.Object,new Mock<ILogger>().Object);
            var result = testCalculator.CalculateTotalStops(StarshipUtils.Create(IntervalUnit.Day, 2, 1), 5);
            Assert.True(result.IsValid);
            Assert.Equal(2, result.NumberOfStops);
        }

        [Fact]
        public void CalculateTotalStops_WhenRangeCalculationZero_ThrowsDivideByZeroException()
        {
            var moqFactory = new Mock<IRangeCalculationStrategyFactory>();
            moqFactory.Setup(mf => mf.GetRangeCalculationStrategy(It.IsAny<IntervalUnit>()))
                .Returns(CreateMoqStrategy(0).Object);

            var testCalculator = new StopsCalculator(moqFactory.Object, new Mock<ILogger>().Object);
            Assert.Throws<DivideByZeroException>(() => testCalculator.CalculateTotalStops(StarshipUtils.Create(IntervalUnit.Day, 2, 1), 1));
        }

        private Mock<IRangeCalculationStrategy> CreateMoqStrategy(int? returnValue)
        {
            var moqStrategy = new Mock<IRangeCalculationStrategy>();
            moqStrategy.Setup(ms => ms.CalculateRange(It.IsAny<Starship>())).Returns(returnValue);
            return moqStrategy;
        }
    }
}
