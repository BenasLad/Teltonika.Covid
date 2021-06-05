using System.Collections.Generic;
using System.Threading.Tasks;
using Teltonika.Covid.Api.Entities;
using Teltonika.Covid.Api.Models;

namespace Teltonika.Covid.Api.Repositories
{
    public interface ICaseRepository
    {
        Task<List<Gender>> GetGendersAsync();
        Task<List<Municipality>> GetMunicipalitiesAsync();
        Task<List<AgeBracket>> GetAgeBracketsAsync();
        Task<List<Case>> GetCasesAsync(int pageSize, int skip, FilterOptions filterOptions);
        Task<int> CreateCaseAsync(CreateCaseRequest caseToCreate);
    }
}
