using System.Threading.Tasks;
using Teltonika.Covid.Api.Models;

namespace Teltonika.Covid.Api.Services
{
    public interface IUserService
    {
        Task<Token> GetAccessToken(CredentialsRequestModel credentials);
    }
}
