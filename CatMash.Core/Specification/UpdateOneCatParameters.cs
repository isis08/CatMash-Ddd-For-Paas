using CatMash.Core.Domain.Entity;

namespace CatMash.Core.Domain.Specification
{
    public class UpdateOneCatParameters : BaseStoredProcedureParameters, IUpdateOneCatParameters
    {
        
        public int Views { get; set; }
        public double Weight { get; set; }
        public double? Rating { get; set; }
        public int? Wins { get; set; }
        public UpdateOneCatParameters()
        {
            StoredProcedure = StoredProceduresEnum.UpdateOneCat;
        }

        public UpdateOneCatParameters(CatEntity cat)
        {
            StoredProcedure = StoredProceduresEnum.UpdateOneCat;

            Id = cat.Id;
            Views = cat.ViewsNumber;
            Weight = cat.ProbabilityWeight;
            Rating = cat.Rating;
            Wins = cat.Wins;
        }
    }
}
