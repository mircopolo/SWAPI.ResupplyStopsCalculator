
using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Swapi.Core.Calculation
{
    /// <summary>
    /// Default implementation of IRangeCalculationStrategy .
    /// </summary>
    public class StopsCalculator : IStopsCalculator
    {
        private ILogger _logger;
        public IRangeCalculationStrategyFactory StrategyFactory { get; private set; }

        public StopsCalculator(IRangeCalculationStrategyFactory strategyFactory, ILogger logger)
        {
            StrategyFactory = strategyFactory;
            _logger = logger;
        }
        /// <summary>
        /// Calculates the number for stops required for the Starship over the total distance.
        /// </summary>
        /// <param name="ship">An instance of a Starship.</param>
        /// <param name="totalDistance">An int representing the total distance of the journey.</param>
        /// <returns>
        /// A StopsCalculationResult containing the result of the calculation.
        /// </returns>
        public StopsCalculationResult CalculateTotalStops(Starship ship, int totalDistance)
        {
            var maximumRange = StrategyFactory.GetRangeCalculationStrategy(ship.ConsumablesIntervalUnit)
                .CalculateRange(ship);

            var result =  new StopsCalculationResult()
            {
                StarshipName = ship.Name,
                IsValid = maximumRange != null
            };

            if (result.IsValid)
            {
                result.NumberOfStops = (int)Math.Floor((double)(totalDistance / maximumRange.Value));
            }

            return result;
        }


    }
}
