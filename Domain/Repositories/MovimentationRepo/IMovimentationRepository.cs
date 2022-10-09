using Library.Domain.Entities;

namespace Library.Domain.Repositories.MovimentationRepo;

public interface IMovimentationRepository
{
    Task<Movimentation> GetMovimentationByIdAsync(Guid id);
    Task<IEnumerable<Movimentation>> GetAllMovimentationsAsync();
    Task<Movimentation> AddMovimentationAsync(Movimentation movimentation);
    Task<Movimentation> UpdateMovimentationAsync(Guid id, Movimentation movimentation);
    Task DeleteMovimentationAsync(Guid id );
}
