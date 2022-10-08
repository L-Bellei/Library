using Library.Domain.Entities;
using Library.Infra;
using Microsoft.EntityFrameworkCore;

namespace Library.Domain.Repositories.LoanRepo;

public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext db;

    public LoanRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task<Loan> AddLoanAsync(Loan loan)
    {
        await db.Loans.AddAsync(loan);

        await db.SaveChangesAsync();

        return loan;
    }

    public async Task DeleteLoanAsync(Guid id)
    {
        Loan? loan = await db.Loans.FirstOrDefaultAsync(l => l.Id == id);

        if(loan != null)
        {
            db.Loans.Remove(loan);
            await db.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        IEnumerable<Loan> loans = await db.Loans.ToListAsync();

        return loans;
    }

    public async Task<Loan?> GetLoanByBookIdAsync(Guid id)
    {
        Loan? loan = await db.Loans.FirstOrDefaultAsync(l => l.BookId == id);

        return loan;
    }

    public async Task<Loan?> GetLoanByIdAsync(Guid id)
    {
        Loan? loan = await db.Loans.FirstOrDefaultAsync(l => l.Id == id);

        return loan;
    }

    public async Task<Loan?> GetLoanByUserIdAsync(Guid id)
    {
        Loan? loan = await db.Loans.FirstOrDefaultAsync(l => l.UserId == id);

        return loan;
    }

    public async Task<Loan> UpdateLoanAsync(Loan loan)
    {
        db.Loans.Update(loan);

        await db.SaveChangesAsync();

        return loan;
    }
}
