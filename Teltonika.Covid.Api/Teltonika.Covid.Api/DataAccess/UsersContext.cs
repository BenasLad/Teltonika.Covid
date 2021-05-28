using Microsoft.EntityFrameworkCore;
using Teltonika.Covid.Api.Entities;

namespace Teltonika.Covid.Api.DataAccess
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
