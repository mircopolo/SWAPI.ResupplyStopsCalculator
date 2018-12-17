using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.UnitTests
{
    public class StarshipUtils
    {
        public static Starship Create(IntervalUnit unit, int? interval, int? maxSpeed )
        {
            return new Starship()
            {
                Name = "TestShip",
                ConsumablesIntervalUnit = unit,
                ConsumablesInterval = interval,
                MaximumSpeed = maxSpeed
            };
        }
    }
}
