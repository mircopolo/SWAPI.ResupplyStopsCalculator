
using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation
{
    /// <summary>
    /// Provides a method for calculating the number of resupply stops required a Starship over a given distance.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public interface IStopsCalculator
    {
        /// <summary>
        /// Calculates the number for stops required for the Starship over the total distance.
        /// </summary>
        /// <param name="ship">An instance of a Starship.</param>
        /// <param name="totalDistance">An int representing the total distance of the journey.</param>
        /// <returns>
        /// A StopsCalculationResult containing the result of the calculation.
        /// </returns>
        StopsCalculationResult CalculateTotalStops(Starship ship, int totalDistance);
    }
}
