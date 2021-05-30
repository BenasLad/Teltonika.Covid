using System.Threading.Tasks;
using Teltonika.Covid.Api.Exceptions;
using Teltonika.Covid.Api.Models;
using Teltonika.Covid.Api.Repositories;

namespace Teltonika.Covid.Api.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<Token> GetAccessToken(CredentialsRequestModel credentials)
        {
            if (!string.IsNullOrEmpty(credentials.Username) 
                && !string.IsNullOrEmpty(credentials.Password) 
                && (await _userRepository.IsLoginValid(credentials.Username, credentials.Password)))
            {
                return new Token { AccessToken = _jwtService.GenerateSecurityToken() };
            }

            throw new UnauthorizedException("Username or password is incorrect");
        }
    }
}
