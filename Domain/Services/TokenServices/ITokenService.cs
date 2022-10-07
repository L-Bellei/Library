using Library.Domain.Entities;
using Library.Domain.Repositories.UserRepo.Dtos;

namespace Library.Domain.Services.TokenServices;

public interface ITokenService
{
    string GenerateToken(UserLoginResponseDto user);
}
