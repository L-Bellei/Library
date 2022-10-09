using Library.Domain.Entities;
using Library.Domain.Repositories.BookRepo;
using Library.Domain.Repositories.PenaltyRepo;
using Library.Domain.Repositories.PenaltyRepo.Dtos;
using Library.Domain.Repositories.UserRepo;

namespace Library.Domain.Services.PenaltyServices;

public class PenaltyService : IPenaltyService
{
    private readonly IPenaltyRepository penaltyRepository;
    private readonly IBookRepository bookRepository;
    private readonly IUserRepository userRepository;

    public PenaltyService(IPenaltyRepository penaltyRepository, IBookRepository bookRepository,
        IUserRepository userRepository)
    {
        this.penaltyRepository = penaltyRepository;
        this.bookRepository = bookRepository;
        this.userRepository = userRepository;
    }

    public async Task<PenaltyAddResponseDto> AddPenaltyAsync(PenaltyAddRequestDto penalty)
    {
        Penalty penaltyAdded = await penaltyRepository.AddPenaltyAsync(new Penalty
        {
            BookId = penalty.BookId,
            PenaltyPrice = penalty.PenaltyPrice,
            Settled = false,
            UserId = penalty.UserId,
        });

        Book book = await bookRepository.GetBookById(penaltyAdded.BookId);
        User user = await userRepository.GetUserAsync(penaltyAdded.UserId);

        if (book != null && user != null)
        {
            return new PenaltyAddResponseDto
            {
                Id = penaltyAdded.Id,
                PenaltyPrice = penaltyAdded.PenaltyPrice,
                BookName = book.Title,
                UserName = user.UserName,
            };
        }
        else
            throw new Exception("Book or User not found");
    }

    public async Task DeletePenaltyAsync(Guid id)
    {
        Penalty? penalty = await penaltyRepository.GetPenaltyByIdAsync(id);
    
        if(penalty != null)
        {
            if (!penalty.Settled)
                await penaltyRepository.DeletePenaltyAsync(penalty.Id);
            else
                throw new Exception("This penalty are not settled yet");
        }
        else
            throw new Exception("Penalty not found");
    }

    public async Task<IEnumerable<PenaltyGetResponseDto>> GetAllNotSettledPenaltiesAsync()
    {
        IEnumerable<Penalty> penalties = await penaltyRepository.GetAllPenaltiesAsync();
        penalties = penalties.Where(p => p.Settled == false).ToList();
        IList<PenaltyGetResponseDto> penaltiesFinded = new List<PenaltyGetResponseDto>();

        if (penalties.Any())
        {
            foreach (var penalty in penalties)
            {
                Book book = await bookRepository.GetBookById(penalty.BookId);
                User user = await userRepository.GetUserAsync(penalty.UserId);

                PenaltyGetResponseDto aux = new()
                {
                    Id = penalty.Id,
                    BookName = book.Title,
                    UserName = user.UserName,
                    PenaltyPrice = penalty.PenaltyPrice,
                    Settled = penalty.Settled,
                };

                penaltiesFinded.Add(aux);
            }

            return penaltiesFinded;
        }
        else
            throw new Exception("No penalties registered yet");
    }

    public async Task<IEnumerable<PenaltyGetResponseDto>> GetAllPenaltiesAsync()
    {
        IEnumerable<Penalty> penalties = await penaltyRepository.GetAllPenaltiesAsync();
        IList<PenaltyGetResponseDto> penaltiesFinded = new List<PenaltyGetResponseDto>();

        if (penalties.Any())
        {
            foreach (var penalty in penalties)
            {
                Book book = await bookRepository.GetBookById(penalty.BookId);
                User user = await userRepository.GetUserAsync(penalty.UserId);

                PenaltyGetResponseDto aux = new()
                {
                    Id = penalty.Id,
                    BookName = book.Title,
                    UserName = user.UserName,
                    PenaltyPrice = penalty.PenaltyPrice,
                    Settled = penalty.Settled,
                };

                penaltiesFinded.Add(aux);
            }

            return penaltiesFinded;
        }
        else
            throw new Exception("No penalties registered yet");
    }

    public async Task<IEnumerable<PenaltyGetResponseDto>> GetAllSettledPenaltiesAsync()
    {
        IEnumerable<Penalty> penalties = await penaltyRepository.GetAllPenaltiesAsync();
        penalties = penalties.Where(p => p.Settled == true).ToList();
        IList<PenaltyGetResponseDto> penaltiesFinded = new List<PenaltyGetResponseDto>();

        if (penalties.Any())
        {
            foreach (var penalty in penalties)
            {
                Book book = await bookRepository.GetBookById(penalty.BookId);
                User user = await userRepository.GetUserAsync(penalty.UserId);

                PenaltyGetResponseDto aux = new()
                {
                    Id = penalty.Id,
                    BookName = book.Title,
                    UserName = user.UserName,
                    PenaltyPrice = penalty.PenaltyPrice,
                    Settled = penalty.Settled,
                };

                penaltiesFinded.Add(aux);
            }

            return penaltiesFinded;
        }
        else
            throw new Exception("No penalties registered yet");
    }

    public async Task<PenaltyGetResponseDto> GetPenaltyByIdAsync(Guid id)
    {
        Penalty? penalty = await penaltyRepository.GetPenaltyByIdAsync(id);

        if (penalty == null)
            throw new Exception("Penalty not found");
        else
        {
            Book book = await bookRepository.GetBookById(penalty.BookId);
            User user = await userRepository.GetUserAsync(penalty.UserId);

            return new PenaltyGetResponseDto
            {
                Id = penalty.Id,
                BookName = book.Title,
                UserName = user.UserName,
                PenaltyPrice = penalty.PenaltyPrice,
                Settled = penalty.Settled,
            };
        }
    }

    public async Task<PenaltyUpdateResponseDto> UpdatePenaltyAsync(Guid id, PenaltyUpdateRequestDto penalty)
    {
        Penalty? penaltyFinded = await penaltyRepository.GetPenaltyByIdAsync(id);
    
        if(penaltyFinded == null)
            throw new Exception("Penalty not found");
        else
        {
            penaltyFinded.PenaltyPrice = penalty.PenaltyPrice;
            penaltyFinded.BookId = penalty.BookId;
            penaltyFinded.UserId = penalty.UserId;
            penaltyFinded.Settled = penalty.Settled;

            await penaltyRepository.UpdatePenaltyAsync(penaltyFinded);

            Book book = await bookRepository.GetBookById(penalty.BookId);
            User user = await userRepository.GetUserAsync(penalty.UserId);

            return new PenaltyUpdateResponseDto
            {
                Id = penaltyFinded.Id,
                BookName = book.Title,
                UserName = user.UserName,
                PenaltyPrice = penaltyFinded.PenaltyPrice,
                Settled = penaltyFinded.Settled,
            };
        }
    }
}
