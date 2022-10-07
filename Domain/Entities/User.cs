namespace Library.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Cpf { get; set; }
    public string Role { get; set; }

    public User() { }

    public User(string username, string email, string password, string cpf, string role)
    {
        this.Id = new Guid();
        this.UserName = username;
        this.Email = email;
        this.Password = password;
        this.Cpf = cpf;
        this.Role = role;
    }
}
