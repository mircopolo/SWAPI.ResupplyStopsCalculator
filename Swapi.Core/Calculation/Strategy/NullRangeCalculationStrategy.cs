using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation.Strategy
{
    /// <summary>
    /// Strategy that always return null, to be used where the maximum consumable duration is not calculable.
    /// </summary>
    public class NullRangeCalculationStrategy : BaseRangeCalculationStrategy
    {
        protected override IntervalUnit ValidUnitType => IntervalUnit.Unknown;

        protected override double? CalculateConsumableDuration(Starship ship)
        {
            return null;
        }
    }
}
