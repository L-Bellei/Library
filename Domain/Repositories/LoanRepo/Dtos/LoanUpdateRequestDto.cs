namespace Library.Domain.Repositories.LoanRepo.Dtos;

public class LoanUpdateRequestDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateTime DeadlineDate { get; set; }
    public DateTime DevolutionDate { get; set; }
    public bool Returned { get; set; }
}
