namespace Library.Domain.Repositories.PenaltyRepo.Dtos;

public class PenaltyUpdateRequestDto
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public Guid LoanId { get; set; }
    public float PenaltyPrice { get; set; }
    public bool Settled { get; set; }
}
