using Library.Domain.Entities;

namespace Library.Domain.Repositories.LoanRepo;

public interface ILoanRepository
{
    Task<Loan?> GetLoanByIdAsync(Guid id); 
    Task<Loan?> GetLoanByBookIdAsync(Guid id); 
    Task<Loan?> GetLoanByUserIdAsync(Guid id);
    Task<IEnumerable<Loan>> GetAllLoansAsync();
    Task<Loan> AddLoanAsync(Loan loan);
    Task<Loan> UpdateLoanAsync(Loan loan);
    Task DeleteLoanAsync(Guid id);
}
