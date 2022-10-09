using Library.Domain.Entities;
using Library.Infra;
using Microsoft.EntityFrameworkCore;

namespace Library.Domain.Repositories.MovimentationRepo;

public class MovimentationRepository : IMovimentationRepository
{
    private readonly ApplicationDbContext db;

    public MovimentationRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task<Movimentation> AddMovimentationAsync(Movimentation movimentation)
    {
        await db.Movimentations.AddAsync(movimentation);
        await db.SaveChangesAsync();

        return movimentation;
    }

    public async Task DeleteMovimentationAsync(Guid id)
    {
        Movimentation? movimentation = await db.Movimentations.FirstOrDefaultAsync(m => m.Id == id);

        if (movimentation == null)
            throw new Exception("Movimentation not found");
        else
        {
            db.Movimentations.Remove(movimentation);
            await db.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Movimentation>> GetAllMovimentationsAsync()
    {
        IEnumerable<Movimentation> movimentations = await db.Movimentations.ToListAsync();

        return movimentations;
    }

    public async Task<Movimentation> GetMovimentationByIdAsync(Guid id)
    {
        Movimentation? movimentation = await db.Movimentations.FirstOrDefaultAsync(m => m.Id == id);

        if (movimentation == null)
            throw new Exception("Movimentation not found");
        else
            return movimentation;
    }

    public async Task<Movimentation> UpdateMovimentationAsync(Guid id, Movimentation movimentation)
    {
        Movimentation? movimentationFinded = await db.Movimentations.FirstOrDefaultAsync(m => m.Id == id);

        if (movimentationFinded == null)
            throw new Exception("Movimentation not found");
        else
        {
            movimentationFinded.MovimentationDate = movimentation.MovimentationDate;
            movimentationFinded.BookId = movimentation.BookId;
            movimentationFinded.UserId = movimentation.UserId;

            db.Movimentations.Update(movimentationFinded);
            await db.SaveChangesAsync();

            return movimentationFinded;
        }
    }
}
