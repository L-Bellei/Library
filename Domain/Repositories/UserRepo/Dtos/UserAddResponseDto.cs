namespace Library.Domain.Repositories.UserRepo.Dtos;

public class UserAddResponseDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}

