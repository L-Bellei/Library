namespace Library.Domain.Repositories.LoanRepo.Dtos;

public class LoanAddRequestDto
{
    public Guid StudentId { get; set; }
    public Guid BookId { get; set; }
}
