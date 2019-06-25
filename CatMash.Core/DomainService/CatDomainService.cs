using System;

namespace CatMash.Core.Domain.DomainService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CatMash.Core.Domain.Business;
    using CatMash.Core.Domain.Entity;
    using CatMash.Core.Domain.Repository;
    using CatMash.Core.Domain.Specification;
    using CatMash.Core.Domain.ValueObject;

    public class CatDomainService
    {
        public readonly IRepository repository;

        //--
        //-- ATTENTION le métier me dit : "L'algorithme de calcul du choix des chats pourra évoluer"
        //--
        private readonly ICatChoiceStrategy catChoiceStrategy;
        
        public CatDomainService(IRepository repository, ICatChoiceStrategy catChoiceStrategy)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.catChoiceStrategy = catChoiceStrategy ?? throw new ArgumentNullException(nameof(catChoiceStrategy));
        }

        public async Task<IEnumerable<CatEntity>> RetrieveTwoRandomCats(FurTypesValueObject? furType = null)
        {
            var parameter = new SelectMultipleCatsParameters(furType: furType);
            var cats = (await repository.GetAsync<CatEntity, SelectMultipleCatsParameters>(parameter)).OrderBy(x => x.ProbabilityWeight);

            var catOneId = await catChoiceStrategy.ChoseCatContestant(cats);
            var catTwoId = await catChoiceStrategy.GetAnotherCat(catOneId, cats);

            var parameters = new SelectTwoCatsParameters(catOneId, catTwoId, furType);
            return await repository.GetAsync<CatEntity, SelectTwoCatsParameters>(parameters);
        }


        //--
        //-- Applicative logic : should be factorized in Applicatio Layer (CatMash.Core.Application)
        //--
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<CatEntity> PatchWinnerCat(CatEntity winner)
        {
            var parameters = new CountViewsParameters();
            int totalViews = await repository.GetOneAsync<int, CountViewsParameters>(parameters);

            winner.ApplySuccess(totalViews);

            var updateParameter = new UpdateOneCatParameters(winner);
            winner = await repository.GetCatAsync<UpdateOneCatParameters>(updateParameter);

            return winner;
        }

        //--
        //-- Applicative logic : should be factorized in Applicatio Layer (CatMash.Core.Application)
        //--
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<CatEntity> PatchLoserCat(CatEntity winner)
        {
            var parameters = new CountViewsParameters();
            int totalViews = await repository.GetOneAsync<int, CountViewsParameters>(parameters);

            winner.ApplyLoose(totalViews);

            var updateParameter = new UpdateOneCatParameters(winner);
            winner = await repository.GetCatAsync<UpdateOneCatParameters>(updateParameter);

            return winner;
        }

    }
}
