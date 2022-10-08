using Library.Domain.Entities;
using Library.Domain.Repositories.BookRepo;
using Library.Domain.Repositories.BookRepo.Dtos;

namespace Library.Domain.Services.BookServices;

public class BookService : IBookService
{
    private readonly IBookRepository bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    public async Task<BookAddResponseDto> AddBookAsync(BookAddRequestDto book)
    {
        if (book.Title == String.Empty || book.Author == String.Empty 
            || book.Subject == String.Empty || book.PublishCompany == String.Empty)
            throw new Exception("All fields must be filled");


        IEnumerable<Book>? books = await bookRepository.GetAllbooksAsync();
        Book bookAdded = new();

        if (books.Any())
        {
            Book? bookFinded = books.Where(b => b.Title == book.Title).FirstOrDefault();

            if (bookFinded == null)
            {
               bookAdded = await bookRepository.AddBookAsync(new Book
                {
                    Author = book.Author,
                    Subject = book.Subject,
                    PublishCompany = book.PublishCompany,
                    Title = book.Title,
                });
            }
            else
                throw new Exception("Book's already registered");
        }
        else
        {
            bookAdded = await bookRepository.AddBookAsync(new Book
            {
                Author = book.Author,
                Subject = book.Subject,
                PublishCompany = book.PublishCompany,
                Title = book.Title,
            });
        }

        return new BookAddResponseDto
        {
            Id = bookAdded.Id,
            Author = bookAdded.Author,
            Subject = bookAdded.Subject,
            PublishCompany = bookAdded.PublishCompany,
            Title = bookAdded.Title,
        };
    }

    public async Task DeleteBookAsync(Guid id)
    {
        await bookRepository.DeleteBookAsync(id);
    }

    public async Task<IEnumerable<BookGetResponseDto>> GetAllBooksAsync()
    {
        IEnumerable<Book>? books = await bookRepository.GetAllbooksAsync();
        IList<BookGetResponseDto> booksResponse = new List<BookGetResponseDto>();

        if (books.Any())
        {
            foreach (var book in books)
            {
                BookGetResponseDto aux = new()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Subject = book.Subject,
                    PublishCompany = book.PublishCompany,
                    Title = book.Title,
                };

                booksResponse.Add(aux);
            }

            return booksResponse;
        }
        else
            throw new Exception("No books registered");
    }

    public async Task<BookGetResponseDto> GetBookByIdAsync(Guid id)
    {
        Book? book = await bookRepository.GetBookById(id);

        if (book == null)
            throw new Exception("Book not found");
        else
            return new BookGetResponseDto
            {
                Id = book.Id,
                Author = book.Author,
                Subject = book.Subject,
                PublishCompany = book.PublishCompany,
                Title = book.Title,
            };
    }

    public async Task<BookUpdateResponseDto> UpdateBookAsync(Guid id, BookUpdateRequestDto book)
    {
        Book bookFinded = await bookRepository.GetBookById(id);

        if (book == null)
            throw new Exception("Book not found");
        else
        {
            Book bookUpdated = await bookRepository.UpdateBookAsync(bookFinded);

            return new BookUpdateResponseDto
            {
                Id = bookUpdated.Id,
                Author = book.Author,
                PublishCompany = book.PublishCompany,
                Subject = book.Subject,
                Title = book.Title,
            };
        }
    }
}
