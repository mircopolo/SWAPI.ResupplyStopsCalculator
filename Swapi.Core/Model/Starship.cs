using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Model
{
    /// <summary>
    /// Simplified model of starship data to support range calculations.
    /// </summary>
    public class Starship 
    {
        /// <summary>
        /// The name of this starship.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The maximum timespan that this ship can operate.
        /// </summary>
        public int? ConsumablesInterval { get; set; }

        /// <summary>
        /// The maximum timespan that this ship can operate.
        /// </summary>
        public IntervalUnit ConsumablesIntervalUnit { get; set; }

        /// <summary>
        /// The maximum speed of this starship, specified in MGTL per hour.
        /// </summary>
        public int? MaximumSpeed { get; set; }
    }
}
