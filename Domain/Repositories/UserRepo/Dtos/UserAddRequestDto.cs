﻿namespace Library.Domain.Repositories.UserRepo.Dtos;

public class UserAddRequestDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Cpf { get; set; }
    public string Role { get; set; }
}

