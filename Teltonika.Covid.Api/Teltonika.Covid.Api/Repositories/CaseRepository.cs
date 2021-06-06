using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teltonika.Covid.Api.DataAccess;
using Teltonika.Covid.Api.Entities;
using Teltonika.Covid.Api.Models;
using Teltonika.Covid.Api.Services;

namespace Teltonika.Covid.Api.Repositories
{
    internal class CaseRepository : ICaseRepository
    {
        private readonly ICovidDbContext _covidContext;

        public CaseRepository(ICovidDbContext covidContext)
        {
            _covidContext = covidContext;
        }

        public async Task<List<AgeBracket>> GetAgeBracketsAsync()
        {
            return await _covidContext.AgeBrackets.ToListAsync();
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _covidContext.Genders.ToListAsync();
        }

        public async Task<List<Municipality>> GetMunicipalitiesAsync()
        {
            return await _covidContext.Municipalities.ToListAsync();
        }

        public async Task<List<Case>> GetCasesAsync(int pageSize, int skip, FilterOptions? filterOptions)
        {
            var query = _covidContext.Cases
                .Include(c => c.AgeBracket)
                .Include(c => c.Gender)
                .Include(c => c.Municipality)
                .AsSplitQuery();
            query = ApplyFilters(query, filterOptions);

            return await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCasesCountAsync(FilterOptions? filterOptions)
        {
            var query = _covidContext.Cases.AsQueryable();
            query = ApplyFilters(query, filterOptions);

            return await query.CountAsync();
        }

        public async Task<int> CreateCaseAsync(CreateCaseRequest caseToCreate)
        {
            var ageBracket = new AgeBracket { Id = caseToCreate.AgeBracket };
            var gender = new Gender { Id = caseToCreate.Gender };
            var municipality = new Municipality { Id = caseToCreate.Municipality };
            _covidContext.AgeBrackets.Attach(ageBracket);
            _covidContext.Genders.Attach(gender);
            _covidContext.Municipalities.Attach(municipality);


            _covidContext.Cases.Add(new Case 
            {
                AgeBracket = ageBracket,
                Gender = gender,
                Municipality = municipality,
                ConfirmationDate = caseToCreate.ConfirmationDate,
                X = caseToCreate.X,
                Y = caseToCreate.Y,
                CaseCode = HashService.ComputeSha256Hash(new Guid().ToString())
            });
            return await _covidContext.SaveChangesAsync();
        }

        private IQueryable<Case> ApplyFilters(IQueryable<Case> query, FilterOptions? filterOptions)
        {
            if (filterOptions != null)
            {
                if (filterOptions.Gender != null)
                    query = query.Where(c => c.Gender!.Id == filterOptions.Gender);

                if (filterOptions.AgeBracket != null)
                    query = query.Where(c => c.AgeBracket!.Id == filterOptions.AgeBracket);

                if (filterOptions.Municipality != null)
                    query = query.Where(c => c.Municipality!.Id == filterOptions.Municipality);

                if (filterOptions.ConfirmationDateFrom != null)
                    query = query.Where(c => c.ConfirmationDate >= filterOptions.ConfirmationDateFrom);

                if (filterOptions.ConfirmationDateTo != null)
                    query = query.Where(c => c.ConfirmationDate <= filterOptions.ConfirmationDateTo);
            }

            return query;
        }
    }
}
