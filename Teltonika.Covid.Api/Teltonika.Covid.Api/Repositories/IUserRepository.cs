using System.Threading.Tasks;

namespace Teltonika.Covid.Api.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsLoginValid(string username, string password);
    }
}
