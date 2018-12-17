using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation.Strategy
{
    /// <summary>
    /// Strategy that calculates a starships maximum consumables duration in hours where this value is specified in months.
    /// </summary>
    public class MonthlyRangeCalculationStrategy : BaseRangeCalculationStrategy
    {
        protected override IntervalUnit ValidUnitType => IntervalUnit.Month;
        protected override double? CalculateConsumableDuration(Starship ship)
        {
            return ship.ConsumablesInterval.Value * 30.44 * 24;
        }
    }
}
