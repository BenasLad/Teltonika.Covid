using Microsoft.EntityFrameworkCore;
using Teltonika.Covid.Api.Entities;

namespace Teltonika.Covid.Api.DataAccess
{
    public class UsersDbContext : DbContext, IUsersDbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
