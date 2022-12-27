using diplomski.Data.Dtos;

namespace diplomski.Services.TokenService
{
    public interface ITokenService
    {
        string GenerateJwt(AdminInputDataDto data);
        Task<bool> AuthenticateUser(AdminInputDataDto data);
    }
}
