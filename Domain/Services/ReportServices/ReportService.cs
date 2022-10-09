using Library.Domain.Entities;
using Library.Domain.Repositories.BookRepo;
using Library.Domain.Repositories.InventoryRepo;
using Library.Domain.Repositories.LoanRepo;
using Library.Domain.Repositories.MovimentationRepo;
using Library.Domain.Repositories.UserRepo;

namespace Library.Domain.Services.ReportServices;

public class ReportService : IReportService
{
    private readonly IBookRepository bookRepository;
    private readonly IUserRepository userRepository;
    private readonly IMovimentationRepository movimentationRepository;
    private readonly ILoanRepository loanRepository;
    private readonly IInventoryRepository inventoryRepository;

    public ReportService(IBookRepository bookRepository, IUserRepository userRepository, IInventoryRepository inventoryRepository,
        IMovimentationRepository movimentationRepository, ILoanRepository loanRepository)
    {
        this.bookRepository = bookRepository;
        this.userRepository = userRepository;
        this.movimentationRepository = movimentationRepository;
        this.loanRepository = loanRepository;
        this.inventoryRepository = inventoryRepository;
    }

    public async Task<IEnumerable<object>> GetInventoryReports()
    {
        IEnumerable<Inventory>? inventories = await inventoryRepository.GetAllInventoryAsync();
        IList<Object> inventoryReports = new List<Object>();

        if (inventories == null)
            throw new Exception("Inventory's empty");
        else
        {
            int i = 0;
            foreach (var inv in inventories)
            {
                Book book = await bookRepository.GetBookById(inv.BookId);

                Object aux = new
                {
                    Order = i,
                    BookName = book.Title,
                    inv.Amount,
                };

                inventoryReports.Add(aux);
                i++;
            }

            return inventoryReports;
        }
    }

    public async Task<IEnumerable<object>> GetLoanByPeriodReports(DateTime? initialDate, DateTime? finalDate)
    {
        IEnumerable<Loan> loans = await loanRepository.GetAllLoansAsync();

        if (initialDate != null)
            loans = loans.Where(l => l.LoanDate >= initialDate);

        if (finalDate != null)
            loans = loans.Where(l => l.DeadlineDate <= finalDate);

        if (loans.Any())
        {
            IList<Object> loanReports = new List<Object>();
            int i = 0;

            foreach (var loan in loans)
            {
                Book book = await bookRepository.GetBookById(loan.BookId);
                User user = await userRepository.GetUserAsync(loan.UserId);

                Object aux = new
                {
                    Order = i,
                    BookName = book.Title,
                    user.UserName,
                    loan.LoanDate,
                    loan.DeadlineDate,
                    loan.DevolutionDate,
                    loan.Returned,
                };

                loanReports.Add(aux);
                i++;
            }

            return loanReports;
        }
        else
            throw new Exception("No loans added yet");
    }

    public async Task<IEnumerable<object>> GetLoanReports()
    {
        IEnumerable<Loan> loans = await loanRepository.GetAllLoansAsync();

        if (loans.Any())
        {
            IList<Object> loanReports = new List<Object>();
            int i = 0;

            foreach (var loan in loans)
            {
                Book book = await bookRepository.GetBookById(loan.BookId);
                User user = await userRepository.GetUserAsync(loan.UserId);

                Object aux = new
                {
                    Order = i,
                    BookName = book.Title,
                    user.UserName,
                    loan.LoanDate,
                    loan.DeadlineDate,
                    loan.DevolutionDate,
                    loan.Returned,
                };

                loanReports.Add(aux);
                i++;
            }

            return loanReports;
        }
        else
            throw new Exception("No loans added yet");
    }

    public async Task<IEnumerable<object>> GetMovimentationByPeriodReports(DateTime? initialDate, DateTime? finalDate)
    {
        IEnumerable<Movimentation> movimentations = await movimentationRepository.GetAllMovimentationsAsync();

        if (initialDate != null)
            movimentations = movimentations.Where(l => l.MovimentationDate >= initialDate);

        if (finalDate != null)
            movimentations = movimentations.Where(l => l.MovimentationDate <= finalDate);

        if (movimentations.Any())
        {
            IList<Object> movimentationReports = new List<Object>();
            int i = 0;

            foreach (var mov in movimentations)
            {
                Book book = await bookRepository.GetBookById(mov.BookId);
                User user = await userRepository.GetUserAsync(mov.UserId);

                Object aux = new
                {
                    Order = i,
                    BookName = book.Title,
                    user.UserName,
                    mov.MovimentationDate,
                };

                movimentationReports.Add(aux);
                i++;
            }

            return movimentationReports;
        }
        else
            throw new Exception("No movimentations ocurred yet");
    }

    public async Task<IEnumerable<object>> GetMovimentationReports()
    {
        IEnumerable<Movimentation> movimentations = await movimentationRepository.GetAllMovimentationsAsync();

        if (movimentations.Any())
        {
            IList<Object> movimentationReports = new List<Object>();
            int i = 0;

            foreach (var mov in movimentations)
            {
                Book book = await bookRepository.GetBookById(mov.BookId);
                User user = await userRepository.GetUserAsync(mov.UserId);

                Object aux = new
                {
                    Order = i,
                    BookName = book.Title,
                    user.UserName,
                    mov.MovimentationDate,
                };

                movimentationReports.Add(aux);
                i++;
            }

            return movimentationReports;
        }
        else
            throw new Exception("No movimentations ocurred yet");
    }
}
