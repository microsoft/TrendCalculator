using System;
using System.Collections.Generic;
using System.Text;

namespace TrendsCalculator.Library.Core.Strategy
{
    /// <summary>
    /// This enum enabled to set the trend calculation strategy 
    /// </summary>
    public enum TrendCalculationStrategy
    {
        /// <summary>
        /// Z Mean Strategy will consider mean of all the Z values and return trend results according to mean Z threshold
        /// </summary>
        ZMean,
        /// <summary>
        /// Custom strategy will return the Z mean but won't filter the results based on any threshold.
        /// </summary>
        Custom
    }
}
