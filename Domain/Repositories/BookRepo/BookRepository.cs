using Library.Domain.Entities;
using Library.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.Domain.Repositories.BookRepo;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext db;

    public BookRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        await db.Books.AddAsync(book);
        await db.SaveChangesAsync();

        return book;
    }

    public async Task DeleteBookAsync(Guid id)
    {
        Book? book = await db.Books.FindAsync(id);
        
        if(book != null)
        {
            db.Books.Remove(book);
            await db.SaveChangesAsync();
        }
        else
            throw new Exception("Book not found");

    }

    public async Task<IEnumerable<Book>> GetAllbooksAsync()
    {
        return await db.Books.ToListAsync();
    }

    public async Task<Book> GetBookById(Guid id)
    {
        Book? book = await db.Books.Where(b => b.Id == id).FirstOrDefaultAsync();

        if (book != null)
        {
            return book;
        }
        else
            throw new Exception("Book not found");
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        EntityEntry<Book> _book = db.Entry(book);

        _book.Property<DateTime>("updatedAt").CurrentValue = DateTime.Now;
        _book.State = EntityState.Modified;

        db.Books.Update(book);
        await db.SaveChangesAsync();

        return book;
    }
}
