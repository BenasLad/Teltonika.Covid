using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teltonika.Covid.Api.Entities;
using Teltonika.Covid.Api.Models;
using Teltonika.Covid.Api.Repositories;

namespace Teltonika.Covid.Api.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepository;
        public CaseService(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }
        public async Task<ListMetadata> GetListMetadata()
        {
            var ageBracketsTask = await _caseRepository.GetAgeBracketsAsync();
            var gendersTask = await _caseRepository.GetGendersAsync();
            var municipalitiesTask = await _caseRepository.GetMunicipalitiesAsync();

            return new ListMetadata()
            {
                AgeBrackets = ageBracketsTask.Select(ageBracket => new Option { Id = ageBracket.Id, Name = ageBracket.Name }),
                Genders = gendersTask.Select(gender => new Option { Id = gender.Id, Name = gender.Name }),
                Municipalities = municipalitiesTask
                    .Select(municipality => new Option { Id = municipality.Id, Name = $"{municipality.Name} - {municipality.Code}" }),
            };
        }

        public async Task<IEnumerable<CaseResponse>> GetCasesAsync(ListOptions listOptions)
        {
            var offset = (listOptions.Page - 1) * listOptions.PageSize;
            var cases = await _caseRepository.GetCasesAsync(listOptions.PageSize, offset, listOptions.Filters );

            return cases.Select(c => new CaseResponse
            {
                Id = c.Id,
                Gender = c.Gender?.Name,
                AgeBracket = c.AgeBracket?.Name,
                Municipality = c.Municipality?.Name != null && c.Municipality.Code != null 
                    ? $"{c.Municipality.Name} - {c.Municipality.Code}" 
                    : null,
                ConfirmationDate = c.ConfirmationDate,
                CaseCode = c.CaseCode,
                X = c.X,
                Y = c.Y
            });
        }

        public async Task<int> CreateCaseAsync(CreateCaseRequest caseToCreate)
        {
            return await _caseRepository.CreateCaseAsync(caseToCreate);
        }
    }
}
