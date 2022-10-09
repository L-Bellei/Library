using Library.Domain.Repositories.PenaltyRepo.Dtos;
using Library.Domain.Services.PenaltyServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PenaltyController : ControllerBase
{
    private readonly IPenaltyService services;

    public PenaltyController(IPenaltyService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("getpenalties")]
    [Authorize(Roles = "Manager, Employee, Student")]
    public async Task<IActionResult> GetAllPenalties()
    {
        try
        {
            IEnumerable<PenaltyGetResponseDto> penalties = await services.GetAllPenaltiesAsync();

            if (penalties.Any())
                return Ok(penalties);
            else
                return NotFound();
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getpenaltybyid")]
    [Authorize(Roles = "Manager, Employee, Student")]
    public async Task<IActionResult> GetPenaltyById([FromHeader] Guid id)
    {
        try
        {
            PenaltyGetResponseDto penalty = await services.GetPenaltyByIdAsync(id);

            if (penalty != null)
                return Ok(penalty);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getsettledpenalties")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> GetAllSettledPenalties()
    {
        try
        {
            IEnumerable<PenaltyGetResponseDto> penalties = await services.GetAllSettledPenaltiesAsync();

            if (penalties.Any())
                return Ok(penalties);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getnotsettledpenalties")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> GetAllNotSettledPenalties()
    {
        try
        {
            IEnumerable<PenaltyGetResponseDto> penalties = await services.GetAllNotSettledPenaltiesAsync();

            if (penalties.Any())
                return Ok(penalties);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("addpenalty")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> AddPenalty([FromBody] PenaltyAddRequestDto penalty)
    {
        try
        {
            PenaltyAddResponseDto penaltyAdded = await services.AddPenaltyAsync(penalty);

            if (penaltyAdded != null)
                return Ok(penaltyAdded);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("updatepenalty")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> UpdatePenalty([FromHeader] Guid id, [FromBody] PenaltyUpdateRequestDto penalty)
    {
        try
        {
            PenaltyUpdateResponseDto penaltyUpdated = await services.UpdatePenaltyAsync(id, penalty);

            if (penaltyUpdated != null)
                return Ok(penaltyUpdated);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("deletepenalty")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> DeletePenalty([FromHeader] Guid id)
    {
        try
        {
            await services.DeletePenaltyAsync(id);
    
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
