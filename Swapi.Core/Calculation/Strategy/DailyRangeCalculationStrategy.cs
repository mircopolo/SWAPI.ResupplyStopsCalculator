﻿using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Calculation.Strategy
{
    /// <summary>
    /// Strategy that calculates a starships maximum consumables duration in hours where this value is specified in days.
    /// </summary>
    public class DailyRangeCalculationStrategy : BaseRangeCalculationStrategy
    {
        protected override IntervalUnit ValidUnitType => IntervalUnit.Day;
        protected override double? CalculateConsumableDuration(Starship ship)
        {
            return ship.ConsumablesInterval.Value * 24;
        }
    }
}
