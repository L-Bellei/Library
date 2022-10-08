using Library.Domain.Repositories.LoanRepo;
using Library.Domain.Repositories.LoanRepo.Dtos;
using Library.Domain.Services.PenaltyServices;

namespace Library.Domain.Services.LoanServices;

public class LoanService : ILoanService
{
    private readonly ILoanRepository loanRepository;
    private readonly IPenaltyService penaltyService;

    public LoanService(ILoanRepository loanRepository, IPenaltyService penaltyService)
    {
        this.loanRepository = loanRepository;
        this.penaltyService = penaltyService;
    }

    public Task<LoanAddResponseDto> AddLoanAsync(LoanAddRequestDto loan)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLoanAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoanGetResponseDto>> GetAllExpiredLoansAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoanGetResponseDto>> GetAllLoansAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoanGetResponseDto>> GetAllLoansByBookAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoanGetResponseDto>> GetAllLoansByDateAsync(DateTime? loanDate, DateTime? deadlineDate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoanGetResponseDto>> GetAllLoansByUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoanGetResponseDto>> GetAllLoansNotReturnedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LoanGetResponseDto>> GetAllLoansReturnedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<LoanGetResponseDto> GetLoanByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<LoanUpdateResponseDto> UpdateLoanAsync(Guid id, LoanUpdateRequestDto loan)
    {
        throw new NotImplementedException();
    }
}
