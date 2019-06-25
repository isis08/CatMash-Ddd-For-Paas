namespace CatMash.API.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CatMash.Core.Domain.DomainService;
    using CatMash.Core.Domain.Entity;
    using CatMash.Core.Domain.Poco;
    using CatMash.Core.Domain.Repository;
    using CatMash.Core.Domain.Specification;
    using CatMash.Core.Domain.ValueObject;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [EnableCors("AllowOrigins")]
    [Route("DddCats")]
    public class CatDddController : Controller
    {
        public readonly IRepository repository;
        private readonly ICatDomainService catService;


        public CatDddController(IRepository repository, ICatDomainService catService)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.catService = catService ?? throw new ArgumentNullException(nameof(catService));
        }

        [HttpGet, Route("{catId}", Name = "GetCat")]
        public async Task<IActionResult> GetCat(int catId)
        {
            var parameters = new SelectOneCatParameters(catId);
            var cat = await repository.GetCatAsync(parameters);

            if (cat != null)
            {
                return Ok(cat);
            }

            return NotFound();
        }

        [HttpGet(Name = "GetCats")]
        public async Task<IActionResult> GetCats()
        {
            //--
            //-- Applicative logic : should be factorized in Applicatio Layer (CatMash.Core.Application)
            //--
            FurTypesValueObject? furType = null;
            var parameters = new SelectMultipleCatsParameters(furType: furType);
            var cats = (await repository.GetAsync<CatEntity, SelectMultipleCatsParameters>(parameters)).OrderByDescending(x => x.ProbabilityWeight);

            if (cats.Count() > 0 && cats != null)
            {
                return Ok(cats);
            }

            return NotFound();
        }

        [HttpGet, Route("{furType}", Name = "GetCatsByFurType")]
        public async Task<IActionResult> GetCatsByFurType(FurTypesValueObject furType)
        {
            //--
            //-- Applicative logic : should be factorized in Applicatio Layer (CatMash.Core.Application)
            //--
            var parameters = new SelectMultipleCatsParameters(furType: furType);
            var cats = (await repository.GetAsync<CatEntity, SelectMultipleCatsParameters>(parameters)).OrderByDescending(x => x.ProbabilityWeight);

            if (cats.Count() > 0 && cats != null)
            {
                return Ok(cats);
            }

            return NotFound();
        }

        [HttpGet, Route("random", Name = "GetTwoRandomCats")]
        public async Task<IActionResult> GetTwoRandomCats()
        {
            var cats = await catService.RetrieveTwoRandomCats();

            if (cats.Count() > 0 && cats != null)
            {
                return Ok(cats);
            }

            return NotFound();
        }

        [HttpGet, Route("random/{furType}", Name = "GetTwoRandomCatsByFur")]
        public async Task<IActionResult> GetTwoRandomCatsByFur(FurTypesValueObject furType)
        {
            var cats = await catService.RetrieveTwoRandomCats(furType);

            if (cats.Count() > 0 && cats != null)
            {
                return Ok(cats);
            }

            return NotFound();
        }

        [HttpPatch(Name = "PatchTwoCats")]
        public async Task<IActionResult> PatchCatsScores([FromBody] PayloadPoco cats)
        {
            var winner = cats.Winner;
            var loser = cats.Loser;
            var winnerCat = await catService.PatchWinnerCat(winner);
            var loserCat = await catService.PatchLoserCat(loser);
            if (winnerCat != null)
            {
                return CreatedAtRoute("GetCat", new { catId = winnerCat.Id }, winnerCat);
            }

            return BadRequest();
        }
    }
}