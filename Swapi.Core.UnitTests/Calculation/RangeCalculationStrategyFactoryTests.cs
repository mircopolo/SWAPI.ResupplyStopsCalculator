using System;
using System.Collections.Generic;
using System.Text;
using Swapi.Core.Calculation;
using Swapi.Core.Calculation.Strategy;
using Swapi.Core.Model;
using Xunit;

namespace Swapi.Core.UnitTests.Calculation
{
    public class RangeCalculationStrategyFactoryTests
    {
        [Theory]
        [MemberData(nameof(UnitStrategyTestData))]
        public void GetRangeCalculationStrategy_ForUnit_ReturnCorrectStrategy(IntervalUnit unit, Type strategyType)
        {
            Assert.IsAssignableFrom(strategyType, new RangeCalculationStrategyFactory().GetRangeCalculationStrategy(unit));
        }

        public static IEnumerable<object[]> UnitStrategyTestData =>
            new List<object[]>
            {
                new object[] { IntervalUnit.Unknown, typeof(NullRangeCalculationStrategy) },
                new object[] { IntervalUnit.Day, typeof(DailyRangeCalculationStrategy) },
                new object[] { IntervalUnit.Week, typeof(WeeklyRangeCalculationStrategy) },
                new object[] { IntervalUnit.Month, typeof(MonthlyRangeCalculationStrategy) },
                new object[] { IntervalUnit.Year, typeof(YearlyRangeCalculationStrategy) },
            };

    }
}
