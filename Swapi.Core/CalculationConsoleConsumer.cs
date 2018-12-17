using Swapi.Core.Calculation;
using System;
using System.Collections.Generic;
using System.Text;


namespace Swapi.Core
{
    /// <summary>
    /// Consumer that writes results to Console.
    /// </summary>
    public class CalculationConsoleConsumer : IObserver<StopsCalculationResult>
    {
        private bool _includeUnknowns;
        public CalculationConsoleConsumer(bool includeUnknowns)
        {
            _includeUnknowns = includeUnknowns;
        }
        /// <summary>
        /// callback for when producer has finished.
        /// </summary>
        public void OnCompleted(){}

        /// <summary>
        /// callback for when producer encounters error.
        /// </summary>
        public void OnError(Exception error){}

        /// <summary>
        /// Callback for processing next result.
        /// </summary>
        public void OnNext(StopsCalculationResult value)
        {

            if (value.IsValid)
            {
                Console.WriteLine("{0}: {1}", value.StarshipName, value.NumberOfStops);
            }
            else
            {
                if(_includeUnknowns)
                    Console.WriteLine("{0}: {1}", value.StarshipName, "unknown");
            }
            
        }
    }
}
