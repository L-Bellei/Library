using Library.Domain.Entities;
using Library.Domain.Repositories.UserRepo.Dtos;

namespace Library.Domain.Services.UserServices;

public interface IUserService
{
    Task<UserLoginResponseDto?> VerifyUserLoginAsync(string email, string password);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid Id);
    Task<UserAddResponseDto> AddUserAsync(UserAddRequestDto user);
    Task<User> UpdateUserAsync(Guid Id, UserUpdateRequestDto user);
    Task DeleteUserAsync(Guid Id);
}
