using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teltonika.Covid.Api.Entities;
using Teltonika.Covid.Api.Exceptions;
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

        public async Task<GetCasesResponse> GetCasesAsync(ListOptions listOptions)
        {
            var offset = (listOptions.Page - 1) * listOptions.PageSize;
            var caseCount = await _caseRepository.GetCasesCountAsync(listOptions.Filters);
            var pageCount = caseCount / listOptions.PageSize + 1;
            var cases = await _caseRepository.GetCasesAsync(listOptions.PageSize, offset, listOptions.Filters );
            var casesResult = cases.Select(c => new CaseModel
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

            return new GetCasesResponse
            {
                Cases = casesResult,
                PageCount = pageCount,
                Page = listOptions.Page
            };
        }

        public async Task<int> CreateCaseAsync(CreateCaseRequest caseToCreate)
        {
            if (!await _caseRepository.ExistsAsync<Gender>(caseToCreate.Gender))
                throw new ValidationException($"Gender with id = {caseToCreate.Gender} does not exist");

            if (!await _caseRepository.ExistsAsync<Gender>(caseToCreate.AgeBracket))
                throw new ValidationException($"AgeBracket with id = {caseToCreate.AgeBracket} does not exist");

            if (!await _caseRepository.ExistsAsync<Gender>(caseToCreate.Municipality))
                throw new ValidationException($"Municipality with id = {caseToCreate.Municipality} does not exist");

            return await _caseRepository.CreateCaseAsync(caseToCreate);
        }
    }
}
