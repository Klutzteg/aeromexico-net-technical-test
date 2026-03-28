using AeromexicoPrueba.Contracts.Requests;
using AeromexicoPrueba.Contracts.Responses;

namespace AeromexicoPrueba.Services;

public interface IAuthService
{
    LoginResponse Login(LoginRequest request);
}
