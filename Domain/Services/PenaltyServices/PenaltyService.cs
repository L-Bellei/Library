using Library.Domain.Repositories.PenaltyRepo;
using Library.Domain.Repositories.PenaltyRepo.Dtos;
using Library.Domain.Services.LoanServices;

namespace Library.Domain.Services.PenaltyServices;

public class PenaltyService : IPenaltyService
{
    private readonly IPenaltyRepository penaltyRepository;

    public PenaltyService(IPenaltyRepository penaltyRepository, ILoanService loanService)
    {
        this.penaltyRepository = penaltyRepository;
    }

    public Task<PenaltyAddResponseDto> AddPenaltyAsync(PenaltyAddRequestDto penalty)
    {
        throw new NotImplementedException();
    }

    public Task DeletePenaltyAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PenaltyGetResponseDto>> GetAllNotSettledPenaltiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PenaltyGetResponseDto>> GetAllPenaltiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PenaltyGetResponseDto>> GetAllSettledPenaltiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PenaltyGetResponseDto> GetPenaltyByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PenaltyUpdateResponseDto> UpdatePenaltyAsync(Guid id, PenaltyUpdateRequestDto penalty)
    {
        throw new NotImplementedException();
    }
}
