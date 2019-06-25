using CatMash.Core.Domain.ValueObject;

namespace CatMash.Core.Domain.Specification
{
    public class SelectTwoCatsParameters : BaseStoredProcedureParameters, ISelectTwoCatsParameters
    {
        public int CatOneId { get; set; }
        public int CatTwoId { get; set; }
        public FurTypesValueObject? FurType { get; set; }
        public SelectTwoCatsParameters()
        {
            StoredProcedure = StoredProceduresEnum.SelectTwoCats;
        }

        public SelectTwoCatsParameters(int catOneId, int catTwoId, FurTypesValueObject? furType = null)
        {
            StoredProcedure = StoredProceduresEnum.SelectTwoCats;

            CatOneId = catOneId;
            FurType = furType;
            CatTwoId = catTwoId;
        }
    }
}
