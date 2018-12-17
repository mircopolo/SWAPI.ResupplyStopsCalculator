using Swapi.Core.Calculation.Strategy;
using System;
using System.Collections.Generic;
using System.Text;
using Swapi.Core.Model;
using Xunit;
using Swapi.Core.Calculation;

namespace Swapi.Core.UnitTests.Calculation.Strategy
{
    public class ConcreteRangeCalculationStrategyTests
    {
        private static readonly IRangeCalculationStrategy DailyStrategy = new DailyRangeCalculationStrategy();
        private static readonly IRangeCalculationStrategy WeeklyStrategy = new WeeklyRangeCalculationStrategy();
        private static readonly IRangeCalculationStrategy MonthlyStrategy = new MonthlyRangeCalculationStrategy();
        private static readonly IRangeCalculationStrategy YearlyStrategy = new YearlyRangeCalculationStrategy();
        private static readonly IRangeCalculationStrategy NullStrategy = new NullRangeCalculationStrategy();

        [Theory]
        [MemberData(nameof(CalculateRangeTestData))]
        public void CalculateRange_OnSuccess_ReturnValidResult(IRangeCalculationStrategy strategy,
            IntervalUnit unit, int intervalValue,int maxSpeed, double? expectedValue)
        {
            var testShip = StarshipUtils.Create(unit, intervalValue, maxSpeed);
            Assert.Equal(expectedValue, strategy.CalculateRange(testShip));
        }

        public static IEnumerable<object[]> CalculateRangeTestData =>
            new List<object[]>
            {
                new object[] { NullStrategy, IntervalUnit.Unknown, null, null, null },
                new object[] { DailyStrategy, IntervalUnit.Day, 1, 1, 24 },
                new object[] { DailyStrategy, IntervalUnit.Day, 2, 1, 48},
                new object[] { DailyStrategy, IntervalUnit.Day, 1, 2, 48},
                new object[] { WeeklyStrategy, IntervalUnit.Week, 1, 1, 168 },
                new object[] { WeeklyStrategy, IntervalUnit.Week, 2, 1, 336 },
                new object[] { WeeklyStrategy, IntervalUnit.Week, 1, 2, 336 },
                new object[] { MonthlyStrategy, IntervalUnit.Month, 1, 1,  731 },
                new object[] { MonthlyStrategy, IntervalUnit.Month, 2, 1,  1461  },
                new object[] { MonthlyStrategy, IntervalUnit.Month, 1, 2,  1461 },
                new object[] { YearlyStrategy, IntervalUnit.Year, 1, 1, 8766 },
                new object[] { YearlyStrategy, IntervalUnit.Year, 2, 1, 17532  },
                new object[] { YearlyStrategy, IntervalUnit.Year, 1, 2, 17532 }
            };
    }
}
