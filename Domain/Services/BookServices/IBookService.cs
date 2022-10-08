using Library.Domain.Repositories.BookRepo.Dtos;

namespace Library.Domain.Services.BookServices;

public interface IBookService
{
    Task<BookAddResponseDto> AddBookAsync(BookAddRequestDto book);
    Task<BookUpdateResponseDto> UpdateBookAsync(Guid id, BookUpdateRequestDto book);
    Task<BookGetResponseDto> GetBookByIdAsync(Guid id);
    Task<IEnumerable<BookGetResponseDto>> GetAllBooksAsync();
    Task DeleteBookAsync(Guid id);
}
