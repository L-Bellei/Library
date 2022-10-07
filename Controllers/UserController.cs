using Library.Domain.Entities;
using Library.Domain.Services.UserServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

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
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            IEnumerable<User> users = await services.GetAllUsersAsync();

            return Ok(users);
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUserById([FromBody] Guid id)
    {
        try
        {
            User user = await services.GetUserByIdAsync(id);

            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        try
        {
            User userCreated = await services.AddUserAsync(user);

            return Created("" ,user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody]Guid id, User user)
    {
        try
        {
            User userUpdated = await services.UpdateUserAsync(id, user);

            return Accepted(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
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
