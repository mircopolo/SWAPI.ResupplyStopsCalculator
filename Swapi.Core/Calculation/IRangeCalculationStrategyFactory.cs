
using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace Swapi.Core.Calculation
{
    /// <summary>
    /// Provides a factory method for returning an appropriate IRangeCalculationStrategy.
    /// </summary>
    /// <remarks>
    /// The range of a ship is specified in Megalights (MGLT) and is calculated as the total number of hours that the ship can provide consumables for the entire crew, 
    /// </remarks>
    public interface IRangeCalculationStrategyFactory
    {
        /// <summary>
        /// Selects the correct strategy implemetation for the specified unit type.
        /// </summary>
        /// <param name="unit">The IntervalUnit used to specify the consumables duration.</param>
        /// <returns>
        /// IRangeCalculationStrategy covering Daily, Weekly, Monthly, Yearly or Unknown cases.
        /// </returns>
        IRangeCalculationStrategy GetRangeCalculationStrategy(IntervalUnit unit);
    }
}
