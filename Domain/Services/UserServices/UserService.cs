using Library.Domain.Entities;
using Library.Domain.Repositories.UserRepo;
using Library.Domain.Repositories.UserRepo.Dtos;

namespace Library.Domain.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<UserLoginResponseDto?> VerifyUserLoginAsync(string email, string password)
    {
        IEnumerable<User>? users = await userRepository.GetAllUsersAsync();

        if (users == null)
            throw new Exception("User Not Found");

        User? user = users.Where(u => u.Email == email).FirstOrDefault();

        if (user == null)
            return null;
        else
        {
            if (user.Password != password)
                throw new Exception("Invalid Email or Password");

            return new UserLoginResponseDto
            {
                Id = user.Id,
                Email = email,
                UserName = user.UserName,
                Role = user.Role,
            };
        }
    }

    public async Task<UserAddResponseDto> AddUserAsync(UserAddRequestDto user)
    {
        IEnumerable<User>? users = await userRepository.GetAllUsersAsync();

        if (users != null)
        {
            User? emailExists = users.Where(u => u.Email == user.Email).FirstOrDefault();
            User? username = users.Where(u => u.UserName == user.UserName).FirstOrDefault();

            if (emailExists != null)
                throw new Exception("User's already exist");

            if (emailExists != null)
                throw new Exception("User's already exist");
        }

        User userAdded = await userRepository.AddUserAsync(new User
        {
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password,
            Cpf = user.Cpf,
            Role = user.Role,
        });

        return new UserAddResponseDto
        {
            Id = userAdded.Id,
            Email = userAdded.Email,
            UserName = userAdded.UserName,
            Role = userAdded.Role,
        };
    }

    public Task DeleteUserAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        IEnumerable<User>? users = await userRepository.GetAllUsersAsync();

        return users;
    }

    public Task<User> GetUserByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(Guid Id, UserUpdateRequestDto user)
    {
        throw new NotImplementedException();
    }
}
