using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Contracts.Responses;

namespace AeromexicoPrueba.Services;

public sealed class AuthService : IAuthService
{
    public LoginResponse Login(LoginRequest request)
    {
        var authorized = request.UserName == "admin" && request.Password == "12345";

        return new LoginResponse
        {
            Authorized = authorized,
            Message = authorized ? "Authorized" : "Unauthorized"
        };
    }
}
