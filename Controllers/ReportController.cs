using Library.Domain.Services.ReportServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ReportController : ControllerBase
{
    private readonly IReportService services;

    public ReportController(IReportService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("inventoryreports")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> GetInventoryReports()
    {
        try
        {
            IEnumerable<Object> report = await services.GetInventoryReports();

            if (report.Any())
                return Ok(report);
            else
                return NotFound();
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("loanreports")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> GetLoanReports()
    {
        try
        {
            IEnumerable<Object> report = await services.GetLoanReports();

            if (report.Any())
                return Ok(report);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("movimentationreports")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> GetMovimentationReports()
    {
        try
        {
            IEnumerable<Object> report = await services.GetMovimentationReports();

            if (report.Any())
                return Ok(report);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("loanbyperiodreports")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> GetLoanByPeriodReports([FromBody] FilterViewModel filter)
    {
        try
        {
            IEnumerable<Object> report = await services.GetLoanByPeriodReports(filter.initialDate, filter.finalDate);

            if (report.Any())
                return Ok(report);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("movimentationbyperiodreports")]
    [Authorize(Roles = "Manager, Employee")]
    public async Task<IActionResult> GetMovimentationByPeriodReports([FromBody] FilterViewModel filter)
    {
        try
        {
            IEnumerable<Object> report = await services.GetMovimentationByPeriodReports(filter.initialDate, filter.finalDate);

            if (report.Any())
                return Ok(report);
            else
                return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
