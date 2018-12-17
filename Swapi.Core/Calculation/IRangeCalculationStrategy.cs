using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation
{
    /// <summary>
    /// Provides a method for strategies to calculating the maximum range of a Starship.
    /// </summary>
    /// <remarks>
    /// The range of a ship is specified in Megalights (MGLT) and is calculated as the total number of hours that the ship can provide consumables for the entire crew, 
    /// multiplied by the maximum number of MGLTs it can fly per hour.
    /// Where the nescessary information to calculate this is not present, implementations should return a null value. 
    /// </remarks>
    public interface IRangeCalculationStrategy
    {
        /// <summary>
        /// Calculates the maximum range in in Megalights (MGLT) of a Starship.
        /// </summary>
        /// <param name="ship">An instance of a Starship.</param>
        /// <returns>
        /// A int representing the number of MGLT.
        /// </returns>
        int? CalculateRange(Starship ship);
    }
}
