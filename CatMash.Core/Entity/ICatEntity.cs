using CatMash.Core.Domain.ValueObject;

namespace CatMash.Core.Domain.Entity
{
    public interface ICatEntity
    {

        void AddFurType(FurTypesValueObject furType);

        void RemoveFurType(FurTypesValueObject furType);

        void ApplySuccess(int totalViews);

        void ApplyLoose(int totalViews);
    }
}
