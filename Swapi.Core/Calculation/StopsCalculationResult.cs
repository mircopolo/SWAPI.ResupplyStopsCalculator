using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation
{
    /// <summary>
    /// Encapsulates the result of a resupply stops calculation.
    /// </summary>
    /// <remarks>
    /// Where there was insufficent information to perform the calculation, the result should marked not valid and the number for stops ignored. 
    /// </remarks>
    public class StopsCalculationResult
    {
        /// <value>Get the validity of calculation.</value>
        public bool IsValid { get; set; }
        
        /// <value>Name of the starship.</value>
        public string StarshipName { get; set; }

        /// <value>Number of stops. Null for invalid calculations.</value>
        public int? NumberOfStops { get; set; }

    }
}
