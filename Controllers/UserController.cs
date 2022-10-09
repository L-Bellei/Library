using Library.Domain.Entities;
using Library.Domain.Repositories.UserRepo.Dtos;
using Library.Domain.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService services;

    public UserController(IUserService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("getusers")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            IEnumerable<UserGetResponseDto> users = await services.GetAllUsersAsync();

            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getuser")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> GetUserById([FromHeader] Guid id)
    {
        try
        {
            UserGetResponseDto user = await services.GetUserByIdAsync(id);

            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("adduser")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> AddUser([FromBody] UserAddRequestDto user)
    {
        try
        {
            UserAddResponseDto userCreated = await services.AddUserAsync(user);

            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("updateuser")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> UpdateUser([FromHeader] Guid id, [FromBody] UserUpdateRequestDto user)
    {
        try
        {
            UserUpdateResponseDto userUpdated = await services.UpdateUserAsync(id, user);

            return Accepted(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("deleteusers")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteUser([FromHeader] Guid id)
    {
        try
        {
            await services.DeleteUserAsync(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
