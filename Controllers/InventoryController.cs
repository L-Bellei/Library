using Library.Domain.Repositories.InventoryRepo.Dtos;
using Library.Domain.Services.InventoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Security.Claims;

namespace Library.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService services;

    public InventoryController(IInventoryService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("getinventory")]
    [Authorize(Roles = "Employee, Manager, Student")]
    public async Task<IActionResult> GetInventory([FromHeader] Guid id)
    {
        try
        {
            InventoryGetResponseDto inventory = await services.GetInventoryAsync(id);

            if (inventory == null)
                return BadRequest("Register not found");
            else
                return Ok(inventory);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getallinventory")]
    [Authorize(Roles = "Employee, Manager, Student")]
    public async Task<IActionResult> GetAllInventory()
    {
        try
        {
            IEnumerable<InventoryGetResponseDto> inventory = await services.GetAllInventoryAsync();

            if (inventory == null)
                return BadRequest("Register not found");
            else
                return Ok(inventory);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("addinventory")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> AddInventory([FromBody] InventoryAddRequestDto inventory)
    {
        try
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var username = identity!.Name;

            InventoryAddResponseDto inventoryAdded = await services.AddInventoryAsync(username!, inventory);

            if (inventoryAdded == null)
                return BadRequest("Register not found");
            else
                return Ok(inventoryAdded);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("updateinventory")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> UpdateInventory([FromHeader] Guid id, [FromBody] InventoryUpdateRequestDto inventory)
    {
        try
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var username = identity!.Name;

            InventoryUpdateResponseDto inventoryUpdated = await services.UpdateInventoryAsync(id, username!, inventory);

            if (inventoryUpdated == null)
                return BadRequest("Register not found");
            else
                return Ok(inventoryUpdated);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("deleteinventory")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> DeleteInventory([FromHeader] Guid id)
    {
        try
        {
            await services.DeleteInventoryAsync(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
