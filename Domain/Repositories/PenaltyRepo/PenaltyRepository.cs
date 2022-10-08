using Library.Domain.Entities;
using Library.Infra;
using Microsoft.EntityFrameworkCore;

namespace Library.Domain.Repositories.PenaltyRepo;

public class PenaltyRepository : IPenaltyRepository
{
    private readonly ApplicationDbContext db;

    public PenaltyRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task<Penalty> AddPenaltyAsync(Penalty penalty)
    {
        await db.Penalties.AddAsync(penalty);

        await db.SaveChangesAsync();
    
        return penalty;
    }

    public async Task DeletePenaltyAsync(Guid id)
    {
        Penalty? penalty = await db.Penalties.FirstOrDefaultAsync(p => p.Id == id);

        if(penalty != null)
        {
            db.Penalties.Remove(penalty);

            await db.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Penalty>> GetAllPenaltiesAsync()
    {
        IEnumerable<Penalty> penalties = await db.Penalties.ToListAsync();

        return penalties;
    }

    public async Task<Penalty?> GetPenaltyByBookIdAsync(Guid id)
    {
        Penalty? penalty = await db.Penalties.FirstOrDefaultAsync(p => p.BookId == id);

        return penalty;
    }

    public async Task<Penalty?> GetPenaltyByIdAsync(Guid id)
    {
        Penalty? penalty = await db.Penalties.FirstOrDefaultAsync(p => p.Id == id);

        return penalty;
    }

    public async Task<Penalty?> GetPenaltyByLoanIdAsync(Guid id)
    {
        Penalty? penalty = await db.Penalties.FirstOrDefaultAsync(p => p.LoanId == id);

        return penalty;
    }

    public async Task<Penalty?> GetPenaltyByUserIdAsync(Guid id)
    {
        Penalty? penalty = await db.Penalties.FirstOrDefaultAsync(p => p.UserId == id);

        return penalty;
    }

    public async Task<Penalty> UpdatePenaltyAsync(Penalty penalty)
    {
        db.Penalties.Update(penalty);
        
        await db.SaveChangesAsync();
    
        return penalty;
    }
}
