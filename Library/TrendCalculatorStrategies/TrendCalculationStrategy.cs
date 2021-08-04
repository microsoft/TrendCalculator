namespace TrendsCalculator.Library.Core.Strategy
{
    /// <summary>
    /// This Enum enabled to set the trend calculation strategy 
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
        Custom,
        /// <summary>
        /// Demand-Supply Srategy will return the trending data considering the demand/supply concept into consideration
        /// </summary>
        DemandSupply
    }
}
