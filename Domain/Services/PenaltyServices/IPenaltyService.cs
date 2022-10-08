using Library.Domain.Repositories.PenaltyRepo.Dtos;

namespace Library.Domain.Services.PenaltyServices;

public interface IPenaltyService
{
    Task<PenaltyGetResponseDto> GetPenaltyByIdAsync(Guid id);
    Task<IEnumerable<PenaltyGetResponseDto>> GetAllPenaltiesAsync();
    Task<IEnumerable<PenaltyGetResponseDto>> GetAllSettledPenaltiesAsync();
    Task<IEnumerable<PenaltyGetResponseDto>> GetAllNotSettledPenaltiesAsync();
    Task<PenaltyAddResponseDto> AddPenaltyAsync(PenaltyAddRequestDto penalty);
    Task<PenaltyUpdateResponseDto> UpdatePenaltyAsync(Guid id, PenaltyUpdateRequestDto penalty);
    Task DeletePenaltyAsync(Guid id);
}
