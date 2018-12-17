using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation.Strategy
{
    /// <summary>
    /// Base Implementation to provide a template for concrete strategies.
    /// </summary>
    /// <remarks>
    /// Concrete implementations should override CalculateConsumableDuration to return the maximum duration in hours, caclulated based on the duration value and unit used.  
    /// </remarks>
    public abstract class BaseRangeCalculationStrategy : IRangeCalculationStrategy
    {
        protected abstract IntervalUnit ValidUnitType { get; }
    
        public int? CalculateRange(Starship ship)
        {
            ValidateInterval(ship);
            var result = ship.MaximumSpeed * CalculateConsumableDuration(ship);
            if (result.HasValue)
            {
                return (int)Math.Round(result.Value);
            }
            return (int?)result;
        }

        protected void ValidateInterval(Starship ship)
        {
            if (ship.ConsumablesIntervalUnit != ValidUnitType)
            {
                throw new CalculationException($"Interval Unit Mismatch. Expected {ValidUnitType.ToString()}, Actual {ship.ConsumablesIntervalUnit}");
            }
        }

        /// <summary>
        /// Calculates the maximum duration in hours that a Starship can provide consumables for th entire crew.
        /// </summary>
        /// <param name="ship">An instance of a Starship.</param>
        /// <returns>
        /// A double representing the number of hours.
        /// </returns>
        protected abstract double? CalculateConsumableDuration(Starship ship);
    }
}
