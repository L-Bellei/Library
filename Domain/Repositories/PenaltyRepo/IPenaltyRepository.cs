using Library.Domain.Entities;

namespace Library.Domain.Repositories.PenaltyRepo;

public interface IPenaltyRepository
{
    Task<Penalty?> GetPenaltyByIdAsync(Guid id);
    Task<Penalty?> GetPenaltyByBookIdAsync(Guid id);
    Task<Penalty?> GetPenaltyByLoanIdAsync(Guid id);
    Task<Penalty?> GetPenaltyByUserIdAsync(Guid id);
    Task<Penalty> AddPenaltyAsync(Penalty penalty);
    Task<Penalty> UpdatePenaltyAsync(Penalty penalty);
    Task<IEnumerable<Penalty>> GetAllPenaltiesAsync();
    Task DeletePenaltyAsync(Guid id);
}
