using WBAPI.API.Models;

namespace WBAPI.API.Services.AuthService
{
    public interface IAuthService
    {
        AuthToken Login(string username, string password);
    }
}
