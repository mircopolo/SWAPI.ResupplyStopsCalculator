using System;
using System.Collections.Generic;
using System.Text;
using Swapi.Core.Calculation;
using Swapi.Core.Calculation.Strategy;
using Swapi.Core.Model;
using Xunit;

namespace Swapi.Core.UnitTests.Calculation.Strategy
{
    public class BaseRangeCalculationStrategyTests
    {
        [Fact]
        public void CalculateRange_WhenUnitMismatch_ThrowsCalculationException()
        {
            var testShip = StarshipUtils.Create(IntervalUnit.Day, 1, 1);
            var strategy = new SimpleRangeCalculationStrategy();
            Assert.Throws<CalculationException>(() => strategy.CalculateRange(testShip));
        }

        [Fact]
        public void CalculateRange_WhenNullInterval_ReturnsNull()
        {
            var testShip = StarshipUtils.Create(IntervalUnit.Unknown, null, null);
            var strategy = new SimpleRangeCalculationStrategy();
            Assert.Null(strategy.CalculateRange(testShip));
        }

        private class SimpleRangeCalculationStrategy : BaseRangeCalculationStrategy
        {
            protected override IntervalUnit ValidUnitType => IntervalUnit.Unknown;

            protected override double? CalculateConsumableDuration(Starship ship)
            {
                return 1.0;
            }
        }
    }
}
