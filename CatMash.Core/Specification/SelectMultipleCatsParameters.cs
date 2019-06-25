using CatMash.Core.Domain.ValueObject;

namespace CatMash.Core.Domain.Specification
{
    public class SelectMultipleCatsParameters : BaseStoredProcedureParameters, ISelectMultipleCatsParameters
    {
        public bool? IsAStar { get; set; }
        public FurTypesValueObject? FurType { get; set; }
        public bool? IsAlone { get; set; }
        public SelectMultipleCatsParameters()
        {
            StoredProcedure = StoredProceduresEnum.SelectMultipleCats;
        }

        public SelectMultipleCatsParameters(bool? isAStar = null, FurTypesValueObject? furType = null, bool? isAlone = null)
        {
            StoredProcedure = StoredProceduresEnum.SelectMultipleCats;

            IsAStar = isAStar;
            FurType = furType;
            IsAlone = isAlone;
        }
    }
}
