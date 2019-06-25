namespace CatMash.Core.Domain.Business
{
    using System;

    public class WeightCalculationSimple : IWeightCalculationStrategy
    {
        public double CalculateWeight(int views, int totalViews)
        {
            double theoricProbability = Convert.ToDouble(totalViews) / 100;
            double theoricWeight = Convert.ToDouble(views) / theoricProbability;

            if (theoricWeight >= 1)
            {
                return 0.01;
            }
            else
            {
                return 1 - theoricWeight;
            }
        }
    }
}
