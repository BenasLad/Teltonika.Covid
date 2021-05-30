using Microsoft.EntityFrameworkCore;
using Teltonika.Covid.Api.Entities;

namespace Teltonika.Covid.Api.DataAccess
{
    public interface IUsersDbContext
    {
        DbSet<User> Users { get; set; }
    }
}
