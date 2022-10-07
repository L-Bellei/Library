using Library.Domain.Entities;

namespace Library.Domain.Services.UserServices;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid Id);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(Guid Id, User user);
    Task DeleteUserAsync(Guid Id);
}
