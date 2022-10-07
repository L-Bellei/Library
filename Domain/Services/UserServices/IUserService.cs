using Library.Domain.Entities;
using Library.Domain.Repositories.UserRepo.Dtos;

namespace Library.Domain.Services.UserServices;

public interface IUserService
{
    Task<UserLoginResponseDto?> VerifyUserLoginAsync(string email, string password);
    Task<IEnumerable<UserGetResponseDto>> GetAllUsersAsync();
    Task<UserGetResponseDto> GetUserByIdAsync(Guid id);
    Task<UserAddResponseDto> AddUserAsync(UserAddRequestDto user);
    Task<UserUpdateResponseDto> UpdateUserAsync(Guid id, UserUpdateRequestDto user);
    Task DeleteUserAsync(Guid id);
}
