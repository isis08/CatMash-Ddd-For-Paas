namespace CatMash.Core.Domain.DomainService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CatMash.Core.Domain.Entity;
    using CatMash.Core.Domain.ValueObject;

    public interface ICatDomainService
    {
        Task<IEnumerable<CatEntity>> RetrieveTwoRandomCats(FurTypesValueObject? furType = null);

        Task<CatEntity> PatchWinnerCat(CatEntity winner);

        Task<CatEntity> PatchLoserCat(CatEntity winner);
    }
}
