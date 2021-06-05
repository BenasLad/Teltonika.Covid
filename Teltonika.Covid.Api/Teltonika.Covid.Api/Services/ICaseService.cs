using System.Collections.Generic;
using System.Threading.Tasks;
using Teltonika.Covid.Api.Models;
using Teltonika.Covid.Api.Repositories;

namespace Teltonika.Covid.Api.Services
{
    public interface ICaseService
    {
        Task<ListMetadata> GetListMetadata();
        Task<IEnumerable<CaseResponse>> GetCasesAsync(ListOptions listOptions);
        Task<int> CreateCaseAsync(CreateCaseRequest caseToCreate);
    }
}
