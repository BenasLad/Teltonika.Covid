using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Teltonika.Covid.Api.DataAccess;

namespace Teltonika.Covid.Api.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly IUsersDbContext _usersContext;

        public UserRepository(IUsersDbContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task<bool> IsLoginValid(string username, string password)
        {
            return await _usersContext.Users.Where(user => user.Username == username && user.Password == password).AnyAsync();
        }
    }
}
