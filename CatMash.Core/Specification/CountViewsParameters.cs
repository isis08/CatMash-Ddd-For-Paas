using CatMash.Core.Domain.ValueObject;

namespace CatMash.Core.Domain.Specification
{
    public class CountViewsParameters : BaseStoredProcedureParameters, ICountViewsParameters
    {
        public FurTypesValueObject? FurType { get;  }
        public CountViewsParameters()
        {
            StoredProcedure = StoredProceduresEnum.CountViews;
        }

        public CountViewsParameters(FurTypesValueObject? furType = null)
        {
            StoredProcedure = StoredProceduresEnum.CountViews;

            FurType = furType;
        }
    }
}
