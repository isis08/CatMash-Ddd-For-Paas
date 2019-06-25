namespace CatMash.Core.Domain.Specification
{
    public class SelectOneCatParameters : BaseStoredProcedureParameters, ISelectOneCatParameters
    {
        public SelectOneCatParameters(int id) : base(id)
        {
            StoredProcedure = StoredProceduresEnum.SelectOneCat;
        }
    }
}
