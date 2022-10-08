using Library.Domain.Entities;
using Library.Domain.Repositories.BookRepo.Dtos;

namespace Library.Domain.Repositories.BookRepo;

public interface IBookRepository 
{
    Task<Book> AddBookAsync(Book book);
    Task<Book> UpdateBookAsync(Book book);
    Task DeleteBookAsync(Guid id);
    Task<Book> GetBookById(Guid id);
    Task<IEnumerable<Book>> GetAllbooksAsync();
}
