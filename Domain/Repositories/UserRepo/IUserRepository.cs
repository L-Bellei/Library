using Library.Domain.Entities;

namespace Library.Domain.Repositories.UserRepo;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(Guid id, User user);
    Task<User> GetUserAsync(Guid id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task DeleteUserAsync(Guid id);
}
