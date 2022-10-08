using Library.Domain.Repositories.LoanRepo.Dtos;
using Library.Domain.Services.LoanServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LoanController : ControllerBase
{
    private readonly ILoanService services;

    public LoanController(ILoanService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("getloans")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> GetAllLoans()
    {
        try
        {
            IEnumerable<LoanGetResponseDto> loans = await services.GetAllLoansAsync();

            if (loans.Any())
                return Ok(loans);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getexpiredloans")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> GetExpiredLoans()
    {
        try
        {
            IEnumerable<LoanGetResponseDto> loans = await services.GetAllExpiredLoansAsync();

            if (loans.Any())
                return Ok(loans);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getnotreturnedloans")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> GetNotReturnedLoans()
    {
        try
        {
            IEnumerable<LoanGetResponseDto> loans = await services.GetAllLoansNotReturnedAsync();

            if (loans.Any())
                return Ok(loans);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getreturnedloans")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> GetReturnedLoans()
    {
        try
        {
            IEnumerable<LoanGetResponseDto> loans = await services.GetAllLoansReturnedAsync();

            if (loans.Any())
                return Ok(loans);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getuserloans")]
    [Authorize(Roles = "Employee, Manager, Student")]
    public async Task<IActionResult> GetUserLoans([FromHeader] Guid id)
    {
        try
        {
            IEnumerable<LoanGetResponseDto> loans = await services.GetAllLoansByUserAsync(id);

            if (loans.Any())
                return Ok(loans);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getbookloans")]
    [Authorize(Roles = "Employee, Manager, Student")]
    public async Task<IActionResult> GetBookLoans([FromHeader] Guid id)
    {
        try
        {
            IEnumerable<LoanGetResponseDto> loans = await services.GetAllLoansByBookAsync(id);

            if (loans.Any())
                return Ok(loans);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getloansbyperiod")]
    [Authorize(Roles = "Employee, Manager, Student")]
    public async Task<IActionResult> GetUserLoans([FromBody] LoanGetByPeriodRequestDto period)
    {
        try
        {
            IEnumerable<LoanGetResponseDto> loans = await services.GetAllLoansByDateAsync(period.loanDate, period.deadlineDate);

            if (loans.Any())
                return Ok(loans);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("addloan")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> AddLoan([FromBody] LoanAddRequestDto loan)
    {
        try
        {
            LoanAddResponseDto loanAdded = await services.AddLoanAsync(loan);

            if (loanAdded != null)
                return Ok(loanAdded);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("updateloan")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> UpdateLoan([FromHeader] Guid id, [FromBody] LoanUpdateRequestDto loan)
    {
        try
        {
            LoanUpdateResponseDto loanUpdated = await services.UpdateLoanAsync(id, loan);

            if (loanUpdated != null)
                return Accepted(loanUpdated);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("deleteloan")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> DeleteLoan([FromHeader] Guid id)
    {
        try
        {
            await services.DeleteLoanAsync(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
