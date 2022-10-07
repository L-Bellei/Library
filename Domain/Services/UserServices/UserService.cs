using Library.Domain.Entities;
using Library.Domain.Repositories.UserRepo;

namespace Library.Domain.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public Task<User> AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(Guid Id, User user)
    {
        throw new NotImplementedException();
    }
}
