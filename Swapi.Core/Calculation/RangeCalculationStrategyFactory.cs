using Swapi.Core.Calculation.Strategy;
using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation
{
    /// <summary>
    /// Default implementation of IRangeCalculationStrategy .
    /// </summary>

    public class RangeCalculationStrategyFactory : IRangeCalculationStrategyFactory
    {
        private static readonly IRangeCalculationStrategy DailyStrategy = new DailyRangeCalculationStrategy();
        private static readonly IRangeCalculationStrategy WeeklyStrategy = new WeeklyRangeCalculationStrategy();
        private static readonly IRangeCalculationStrategy MonthlyStrategy = new MonthlyRangeCalculationStrategy();
        private static readonly IRangeCalculationStrategy YearlyStrategy = new YearlyRangeCalculationStrategy();
        private static readonly IRangeCalculationStrategy NullStrategy = new NullRangeCalculationStrategy();

        /// <summary>
        /// Return the correct implementation of IRangeCalculationStrategy .
        /// </summary>
        /// <param name="unit">The interval unit.</param>
        /// <returns>
        /// A calculation stragtegy appropriate to the unit type.
        /// </returns>
        public IRangeCalculationStrategy GetRangeCalculationStrategy(IntervalUnit unit)
        {
            switch (unit)
            {
                case IntervalUnit.Day:
                    return DailyStrategy;
                case IntervalUnit.Week:
                    return WeeklyStrategy;
                case IntervalUnit.Month:
                    return MonthlyStrategy;
                case IntervalUnit.Year:
                    return YearlyStrategy;
                default:
                    return NullStrategy;
            }
        }
    }
}
