namespace Library.Domain.Repositories.LoanRepo.Dtos;

public class LoanGetByPeriodRequestDto
{
    public DateTime? loanDate { get; set; }
    public DateTime? deadlineDate { get; set; }
}
