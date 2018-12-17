using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation.Strategy
{
    /// <summary>
    /// Strategy that calculates a starships maximum consumables duration in hours where this value is specified in years.
    /// </summary>
    public class YearlyRangeCalculationStrategy : BaseRangeCalculationStrategy
    {
        protected override IntervalUnit ValidUnitType => IntervalUnit.Year;

        protected override double? CalculateConsumableDuration(Starship ship)
        {
            return ship.ConsumablesInterval.Value * 365.25 * 24;
        }
    }
}
