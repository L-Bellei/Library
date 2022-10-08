using Library.Domain.Repositories.BookRepo.Dtos;
using Library.Domain.Services.BookServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BookController : ControllerBase
{
    private readonly IBookService services;

    public BookController(IBookService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("getbooks")]
    [Authorize(Roles = "Employee, Manager, Student")]
    public async Task<IActionResult> GetAllBooks()
    {
        try
        {
            IEnumerable<BookGetResponseDto> books = await services.GetAllBooksAsync();

            if (books == null)
                return NotFound();
            else
                return Ok(books);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("getbook")]
    [Authorize(Roles = "Employee, Manager, Student")]
    public async Task<IActionResult> GetBook([FromHeader] Guid id)
    {
        try
        {
            BookGetResponseDto book = await services.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();
            else
                return Ok(book);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("addbook")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> AddBook([FromBody] BookAddRequestDto book)
    {
        try
        {
            BookAddResponseDto bookAdded = await services.AddBookAsync(book);

            if (bookAdded == null)
                return BadRequest();
            else
                return Ok(bookAdded);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("updatebook")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> UpdateBook([FromHeader] Guid id, [FromBody] BookUpdateRequestDto book)
    {
        try
        {
            BookUpdateResponseDto bookUpdated = await services.UpdateBookAsync(id, book);

            if (bookUpdated == null)
                return BadRequest();
            else
                return Accepted(bookUpdated);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("deletebook")]
    [Authorize(Roles = "Employee, Manager")]
    public async Task<IActionResult> DeleteBook([FromHeader] Guid id)
    {
        try
        {
            await services.DeleteBookAsync(id);

            return NoContent();
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
