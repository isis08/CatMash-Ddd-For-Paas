namespace CatMash.Core.Domain.Business
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CatMash.Core.Domain.Entity;

    public interface ICatChoiceStrategy
    {
        Task<int> ChoseCatContestant(IEnumerable<CatEntity> cats);

        Task<int> GetAnotherCat(int catOneId, IEnumerable<CatEntity> cats);
    }
}
