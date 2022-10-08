using Library.Domain.Repositories.LoanRepo.Dtos;

namespace Library.Domain.Services.LoanServices;

public interface ILoanService
{
    Task<LoanGetResponseDto> GetLoanByIdAsync(Guid id);
    Task<IEnumerable<LoanGetResponseDto>> GetAllLoansAsync();
    Task<LoanAddResponseDto> AddLoanAsync(LoanAddRequestDto loan);
    Task<LoanUpdateResponseDto> UpdateLoanAsync(Guid id, LoanUpdateRequestDto loan);
    Task<IEnumerable<LoanGetResponseDto>> GetAllLoansNotReturnedAsync();
    Task<IEnumerable<LoanGetResponseDto>> GetAllLoansReturnedAsync();
    Task<IEnumerable<LoanGetResponseDto>> GetAllLoansByUserAsync(Guid id);
    Task<IEnumerable<LoanGetResponseDto>> GetAllLoansByBookAsync(Guid id);
    Task<IEnumerable<LoanGetResponseDto>> GetAllLoansByDateAsync(DateTime? loanDate, DateTime? deadlineDate);
    Task<IEnumerable<LoanGetResponseDto>> GetAllExpiredLoansAsync();
    Task DeleteLoanAsync(Guid id);
}
