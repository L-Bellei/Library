using Library.Domain.Entities;
using Library.Domain.Repositories.UserRepo.Dtos;
using Library.Domain.Services.TokenServices;
using Library.Domain.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LoginController : ControllerBase
{
    private readonly IUserService service;
    private readonly ITokenService tokenService;

    public LoginController(IUserService service, ITokenService tokenService)
    {
        this.service = service;
        this.tokenService = tokenService;   
    }

    [HttpPost]
    [Route("auth")]
    [AllowAnonymous]
    public async Task<IActionResult> AuthenticateAsync([FromBody] UserLoginRequestDto userLogin)
    {
        try
        {
            UserLoginResponseDto? userLogged = await service.VerifyUserLoginAsync(userLogin.Email, userLogin.Password);

            if (userLogged == null)
                return NotFound("Invalid Email or Password");

            var token = tokenService.GenerateToken(userLogged);

            UserLoginResponseDto userResponse = new()
            { 
                Id = userLogged.Id,
                Email = userLogged.Email,
                UserName = userLogged.UserName,
                Role = userLogged.Role,
            };

            return Ok(new { user = userResponse, token });
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
