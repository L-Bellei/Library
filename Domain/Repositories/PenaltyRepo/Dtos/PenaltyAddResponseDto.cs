namespace Library.Domain.Repositories.PenaltyRepo.Dtos;

public class PenaltyAddResponseDto
{
    public Guid Id { get; set; }
    public string BookName { get; set; }
    public string UserName { get; set; }
    public Guid LoanId { get; set; }
    public float PenaltyPrice { get; set; }
}
