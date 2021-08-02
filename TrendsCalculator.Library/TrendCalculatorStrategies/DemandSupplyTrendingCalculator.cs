using System;
using System.Collections.Generic;
using System.Text;
using TrendsCalculator.Library.AlgoComponents.GlobalZCalculationCriterias;

namespace TrendsCalculator.Library.TrendingCalculatorForModelsStrategy
{
    internal class DemandSupplyTrendingCalculator : AbstractTrendingCalculator
    {
        internal override IGlobalZCalculationCriteria GetAlgoConstruct()
        {
            return new GlobalZCalculationCustomCriteria();
        }

        internal override List<T> PostProcessZScore<T>(List<T> trendingModels)
        {
            return trendingModels;
        }
    }
}
