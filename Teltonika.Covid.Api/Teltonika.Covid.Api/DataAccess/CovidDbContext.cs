using Microsoft.EntityFrameworkCore;
using Teltonika.Covid.Api.Entities;

namespace Teltonika.Covid.Api.DataAccess
{
    public class CovidDbContext : DbContext, ICovidDbContext
    {
        public CovidDbContext(DbContextOptions<CovidDbContext> options) : base(options) { }

        public DbSet<Case> Cases { get; set; }
        public DbSet<AgeBracket> AgeBrackets { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Gender> Genders { get; set; }
    }
}
