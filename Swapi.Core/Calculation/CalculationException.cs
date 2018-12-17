using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation
{    
    /// <summary>
    /// Exception that indication an error occured during calculations.
    /// </summary>

    public class CalculationException : Exception
    {
        /// <summary>
        /// Create a new exception.
        /// </summary>
        /// <param name="message">Error mesage.</param>

        public CalculationException(string message) : base(message){}

        /// <summary>
        /// Create a new exception.
        /// </summary>
        /// <param name="message">Error mesage.</param>
        /// <param name="innerException">Root cause exception</param>
        public CalculationException(string message,Exception innerException) : base(message,innerException) { }
    }
}
