namespace CatMash.Core.Domain.Business
{
    public interface IWeightCalculationStrategy
    {
        double CalculateWeight(int views, int totalViews);
    }
}
