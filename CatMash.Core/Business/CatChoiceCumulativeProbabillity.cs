namespace CatMash.Core.Domain.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CatMash.Core.Domain.Entity;
    using CatMash.Core.Domain.Repository;
    using CatMash.Core.Domain.Specification;


    public class CatChoiceCumulativeProbabillity : ICatChoiceStrategy
    {

        public readonly IRepository repository;

        public CatChoiceCumulativeProbabillity(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> ChoseCatContestant(IEnumerable<CatEntity> cats)
        {
            var parameters = new GetTotalWeightParameters();
            double totalWeight = await repository.GetOneAsync<double, GetTotalWeightParameters>(parameters);
            var random = new Random();
            double randomValue = random.NextDouble() * totalWeight;
            double cumulativeProbability = 0.0;

            foreach (var cat in cats)
            {
                cumulativeProbability += cat.ProbabilityWeight;
                if (randomValue < cumulativeProbability)
                {
                    return cat.Id;
                }
            }

            return cats.Last().Id;
        }

        public async Task<int> GetAnotherCat(int catOneId, IEnumerable<CatEntity> cats)
        {
            int catTwoId = await ChoseCatContestant(cats);
            if (catOneId == catTwoId)
            {
                return await GetAnotherCat(catOneId, cats);
            }
            return catTwoId;
        }
    }
}
