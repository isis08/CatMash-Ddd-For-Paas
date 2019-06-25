namespace CatMash.Core.Domain.Specification
{
    public class GetTotalWeightParameters : BaseStoredProcedureParameters, IGetTotalWeightParameters
    {
        public GetTotalWeightParameters()
        {
            StoredProcedure = StoredProceduresEnum.GetTotalWeight;
        }
    }
}
