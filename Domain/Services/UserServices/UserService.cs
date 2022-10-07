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

    public async Task DeleteUserAsync(Guid id)
    {
        await userRepository.DeleteUserAsync(id);
    }

    public async Task<IEnumerable<UserGetResponseDto>> GetAllUsersAsync()
    {
        IEnumerable<User>? users = await userRepository.GetAllUsersAsync();
        IList<UserGetResponseDto> usersResponse = new List<UserGetResponseDto>();

        if (users == null)
            throw new Exception("No users registered");

        foreach (var user in users)
        {
            UserGetResponseDto aux = new()
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                UserName = user.UserName,
            };

            usersResponse.Add(aux);
        }

        return usersResponse;
    }

    public async Task<UserGetResponseDto> GetUserByIdAsync(Guid id)
    {
        User? userFinded = await userRepository.GetUserAsync(id);

        if (userFinded == null)
            throw new Exception("User not found");

        return new UserGetResponseDto
        {
            Id = userFinded.Id,
            UserName = userFinded.UserName,
            Email = userFinded.Email,
            Role = userFinded.Role,
        };


    }

    public async Task<UserUpdateResponseDto> UpdateUserAsync(Guid id, UserUpdateRequestDto user)
    {
        IEnumerable<User>? users = await userRepository.GetAllUsersAsync();

        if (users == null)
            throw new Exception("User not found");
        else
        {
            User? userFinded = users.Where(x => x.Id == id).FirstOrDefault();

            if (user == null)
                throw new Exception("User not found");
            else
            {
                userFinded!.UserName = user.UserName;
                userFinded.Email = user.Email;
                userFinded.Role = user.Role;
                userFinded.Cpf = user.Cpf;
            }

            User userUpdated = await userRepository.UpdateUserAsync(userFinded);

            return new UserUpdateResponseDto
            {
              Id = userUpdated.Id,
              Email = userUpdated.Email,    
              UserName = userUpdated.UserName,
              Role = userUpdated.Role,  
            };
        }
    }
}
