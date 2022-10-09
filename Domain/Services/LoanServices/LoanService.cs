using Library.Domain.Entities;
using Library.Domain.Repositories.BookRepo;
using Library.Domain.Repositories.InventoryRepo;
using Library.Domain.Repositories.LoanRepo;
using Library.Domain.Repositories.LoanRepo.Dtos;
using Library.Domain.Repositories.PenaltyRepo;
using Library.Domain.Repositories.UserRepo;

namespace Library.Domain.Services.LoanServices;

public class LoanService : ILoanService
{
    private readonly ILoanRepository loanRepository;
    private readonly IInventoryRepository inventoryRepository;
    private readonly IBookRepository bookRepository;
    private readonly IUserRepository userRepository;
    private readonly IPenaltyRepository penaltyRepository;

    public LoanService(ILoanRepository loanRepository, IInventoryRepository inventoryRepository,
        IBookRepository bookRepository, IUserRepository userRepository, IPenaltyRepository penaltyRepository)
    {
        this.loanRepository = loanRepository;
        this.inventoryRepository = inventoryRepository;
        this.bookRepository = bookRepository;
        this.userRepository = userRepository;
        this.penaltyRepository = penaltyRepository;
    }

    public async Task<LoanAddResponseDto> AddLoanAsync(LoanAddRequestDto loan)
    {
        Inventory? inventory = await inventoryRepository.GetInventoryByBookIdAsync(loan.BookId);
        Penalty? userPenalty = await penaltyRepository.GetPenaltyByUserIdAsync(loan.StudentId);

        if (userPenalty == null || userPenalty.Settled == true)
        {
            if (inventory != null)
            {
                if (inventory.Amount > 0)
                {
                    Loan loanAdded = await loanRepository.AddLoanAsync(new Loan
                    {
                        BookId = loan.BookId,
                        UserId = loan.StudentId,
                        LoanDate = DateTime.Now,
                        DeadlineDate = DateTime.Now.AddDays(5),
                        Returned = false,
                    });

                    Book book = await bookRepository.GetBookById(loan.BookId);
                    User user = await userRepository.GetUserAsync(loan.StudentId);

                    return new LoanAddResponseDto
                    {
                        Id = loanAdded.Id,
                        BookName = book.Title,
                        UserName = user.UserName,
                        LoanDate = loanAdded.LoanDate,
                        DeadlineDate = loanAdded.DeadlineDate,
                        Returned = loanAdded.Returned,
                    };
                }
                else
                    throw new Exception("Haven't books in inventory");
            }
            else
                throw new Exception("Haven't books in inventory");
        }
        throw new Exception("The student have penalties not settleds");
    }

    public async Task DeleteLoanAsync(Guid id)
    {
        Loan? loanFinded = await loanRepository.GetLoanByIdAsync(id);

        if (loanFinded != null)
        {
            Penalty? penaltyFinded = await penaltyRepository.GetPenaltyByUserIdAsync(loanFinded.UserId);

            if (penaltyFinded != null)
            {
                if (penaltyFinded.Settled == false)
                    throw new Exception("Is not possible to delete, ");
                else
                    await loanRepository.DeleteLoanAsync(id);
            }
            else
                await loanRepository.DeleteLoanAsync(id);
        }
        else
            throw new Exception("Loan not found");
    }

    public async Task<IEnumerable<LoanGetResponseDto>> GetAllExpiredLoansAsync()
    {
        IEnumerable<Loan> loans = await loanRepository.GetAllLoansAsync();

        IList<Loan> loansExpireds = loans.Where(l => l.DeadlineDate > DateTime.Now).ToList();
        IList<LoanGetResponseDto> loansResponse = new List<LoanGetResponseDto>();

        foreach (var loan in loansExpireds)
        {
            if (!loan.Returned)
            {
                Book book = await bookRepository.GetBookById(loan.BookId);
                User user = await userRepository.GetUserAsync(loan.UserId);

                LoanGetResponseDto aux = new LoanGetResponseDto
                {
                    Id = loan.Id,
                    BookName = book.Title,
                    UserName = user.UserName,
                    LoanDate = loan.LoanDate,
                    DeadlineDate = loan.DeadlineDate,
                    DevolutionDate = loan.DevolutionDate,
                    Returned = loan.Returned,
                };

                loansResponse.Add(aux);
            }
        }
        return loansResponse;
    }

    public async Task<IEnumerable<LoanGetResponseDto>> GetAllLoansAsync()
    {
        IEnumerable<Loan> loans = await loanRepository.GetAllLoansAsync();
        IList<LoanGetResponseDto> loansResponse = new List<LoanGetResponseDto>();

        foreach (var loan in loans)
        {
            Book book = await bookRepository.GetBookById(loan.BookId);
            User user = await userRepository.GetUserAsync(loan.UserId);

            LoanGetResponseDto aux = new LoanGetResponseDto
            {
                Id = loan.Id,
                BookName = book.Title,
                UserName = user.UserName,
                DeadlineDate = loan.DeadlineDate,
                DevolutionDate = loan.DevolutionDate,
                LoanDate = loan.LoanDate,
                Returned = loan.Returned,
            };

            loansResponse.Add(aux);
        }

        return loansResponse;

    }

    public async Task<IEnumerable<LoanGetResponseDto>> GetAllLoansByBookAsync(Guid id)
    {
        IEnumerable<Loan> loans = await loanRepository.GetAllLoansAsync();
        IList<Loan> loansFinded = loans.Where(l => l.BookId == id).ToList();
        IList<LoanGetResponseDto> loansResponse = new List<LoanGetResponseDto>();

        foreach (var loan in loansFinded)
        {
            Book book = await bookRepository.GetBookById(loan.BookId);
            User user = await userRepository.GetUserAsync(loan.UserId);

            LoanGetResponseDto aux = new LoanGetResponseDto
            {
                Id = loan.Id,
                BookName = book.Title,
                UserName = user.UserName,
                DeadlineDate = loan.DeadlineDate,
                DevolutionDate = loan.DevolutionDate,
                LoanDate = loan.LoanDate,
                Returned = loan.Returned,
            };

            loansResponse.Add(aux);
        }

        return loansResponse;
    }

    public async Task<IEnumerable<LoanGetResponseDto>> GetAllLoansByDateAsync(DateTime? loanDate, DateTime? deadlineDate)
    {
        IEnumerable<Loan> loans = await loanRepository.GetAllLoansAsync();

        if (loanDate != null)
            loans = loans.Where(l => l.LoanDate >= loanDate).ToList();

        if (deadlineDate != null)
            loans = loans.Where(l => l.DeadlineDate <= deadlineDate).ToList();

        IList<LoanGetResponseDto> loansResponse = new List<LoanGetResponseDto>();

        foreach (var loan in loans)
        {
            Book book = await bookRepository.GetBookById(loan.BookId);
            User user = await userRepository.GetUserAsync(loan.UserId);

            LoanGetResponseDto aux = new LoanGetResponseDto
            {
                Id = loan.Id,
                BookName = book.Title,
                UserName = user.UserName,
                DeadlineDate = loan.DeadlineDate,
                DevolutionDate = loan.DevolutionDate,
                LoanDate = loan.LoanDate,
                Returned = loan.Returned,
            };

            loansResponse.Add(aux);
        }

        return loansResponse;
    }

    public async Task<IEnumerable<LoanGetResponseDto>> GetAllLoansByUserAsync(Guid id)
    {
        IEnumerable<Loan> loans = await loanRepository.GetAllLoansAsync();

        loans = loans.Where(l => l.UserId == id).ToList();

        IList<LoanGetResponseDto> loansResponse = new List<LoanGetResponseDto>();

        foreach (var loan in loans)
        {
            Book book = await bookRepository.GetBookById(loan.BookId);
            User user = await userRepository.GetUserAsync(loan.UserId);

            LoanGetResponseDto aux = new LoanGetResponseDto
            {
                Id = loan.Id,
                BookName = book.Title,
                UserName = user.UserName,
                DeadlineDate = loan.DeadlineDate,
                DevolutionDate = loan.DevolutionDate,
                LoanDate = loan.LoanDate,
                Returned = loan.Returned,
            };

            loansResponse.Add(aux);
        }

        return loansResponse;
    }

    public async Task<IEnumerable<LoanGetResponseDto>> GetAllLoansNotReturnedAsync()
    {
        IEnumerable<Loan> loans = await loanRepository.GetAllLoansAsync();

        loans = loans.Where(l => l.Returned == false).ToList();

        IList<LoanGetResponseDto> loansResponse = new List<LoanGetResponseDto>();

        foreach (var loan in loans)
        {
            Book book = await bookRepository.GetBookById(loan.BookId);
            User user = await userRepository.GetUserAsync(loan.UserId);

            LoanGetResponseDto aux = new LoanGetResponseDto
            {
                Id = loan.Id,
                BookName = book.Title,
                UserName = user.UserName,
                DeadlineDate = loan.DeadlineDate,
                DevolutionDate = loan.DevolutionDate,
                LoanDate = loan.LoanDate,
                Returned = loan.Returned,
            };

            loansResponse.Add(aux);
        }

        return loansResponse;
    }

    public async Task<IEnumerable<LoanGetResponseDto>> GetAllLoansReturnedAsync()
    {
        IEnumerable<Loan> loans = await loanRepository.GetAllLoansAsync();

        loans = loans.Where(l => l.Returned == true).ToList();

        IList<LoanGetResponseDto> loansResponse = new List<LoanGetResponseDto>();

        foreach (var loan in loans)
        {
            Book book = await bookRepository.GetBookById(loan.BookId);
            User user = await userRepository.GetUserAsync(loan.UserId);

            LoanGetResponseDto aux = new LoanGetResponseDto
            {
                Id = loan.Id,
                BookName = book.Title,
                UserName = user.UserName,
                DeadlineDate = loan.DeadlineDate,
                DevolutionDate = loan.DevolutionDate,
                LoanDate = loan.LoanDate,
                Returned = loan.Returned,
            };

            loansResponse.Add(aux);
        }

        return loansResponse;
    }

    public async Task<LoanGetResponseDto> GetLoanByIdAsync(Guid id)
    {
        Loan? loan = await loanRepository.GetLoanByIdAsync(id);

        if (loan != null)
        {
            Book book = await bookRepository.GetBookById(loan.BookId);
            User user = await userRepository.GetUserAsync(loan.UserId);

            return new LoanGetResponseDto
            {
                Id = loan.Id,
                BookName = book.Title,
                UserName = user.UserName,
                DeadlineDate = loan.DeadlineDate,
                DevolutionDate = loan.DevolutionDate,
                LoanDate = loan.LoanDate,
                Returned = loan.Returned,
            };
        }
        else
            throw new Exception("Loan not found");
    }

    public async Task<LoanUpdateResponseDto> UpdateLoanAsync(Guid id, LoanUpdateRequestDto loan)
    {
        Loan? loanFinded = await loanRepository.GetLoanByIdAsync(id);

        if (loanFinded != null)
        {
            loanFinded.DeadlineDate = loan.DeadlineDate;
            loanFinded.DevolutionDate = loan.DevolutionDate;
            loanFinded.Returned = loan.Returned;
            loanFinded.BookId = loan.BookId;
            loanFinded.UserId = loan.UserId;

            Loan loanUpdated = await loanRepository.UpdateLoanAsync(loanFinded);

            Book book = await bookRepository.GetBookById(loanUpdated.BookId);
            User user = await userRepository.GetUserAsync(loanUpdated.UserId);

            return new LoanUpdateResponseDto
            {
                Id = loanUpdated.Id,
                BookName = book.Title,
                UserName = user.UserName,
                LoanDate = loanUpdated.LoanDate,
                DeadlineDate = loanUpdated.DeadlineDate,
                DevolutionDate = loanUpdated.DevolutionDate,
                Returned = loanUpdated.Returned,
            };
        }
        else
            throw new Exception("Loan not found");
    }
}
