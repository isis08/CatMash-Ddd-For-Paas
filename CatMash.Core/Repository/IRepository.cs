using System.Collections.Generic;
using System.Threading.Tasks;
using CatMash.Core.Domain.Entity;
using CatMash.Core.Domain.Specification;

namespace CatMash.Core.Domain.Repository
{
    public interface IRepository
    {
        Task<CatEntity> GetCatAsync<Parameters>(Parameters parameters)
            where Parameters : IBaseStoredProcedureParameters;

        Task<IEnumerable<Response>> GetAsync<Response, Parameters>(Parameters parameters)
            where Parameters : IBaseStoredProcedureParameters;

        Task<Response> GetOneAsync<Response, Parameters>(Parameters parameters)
            where Parameters : IBaseStoredProcedureParameters;
    }
}