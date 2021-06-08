using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Teltonika.Covid.Api.Entities;

namespace Teltonika.Covid.Api.DataAccess
{
    public interface ICovidDbContext
    {
        DbSet<Case> Cases { get; set; }
        DbSet<AgeBracket> AgeBrackets { get; set; }
        DbSet<Municipality> Municipalities { get; set; }
        DbSet<Gender> Genders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
