namespace Library.Domain.Repositories.PenaltyRepo.Dtos;

public class PenaltyAddRequestDto
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public Guid LoanId { get; set; }
    public float PenaltyPrice { get; set; }
}
