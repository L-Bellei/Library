namespace Library.Domain.Repositories.LoanRepo.Dtos;

public class LoanGetResponseDto
{
    public Guid Id { get; set; }
    public string BookName { get; set; }
    public string UserName { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DeadlineDate { get; set; }
    public DateTime? DevolutionDate { get; set; }
    public bool Returned { get; set; }
}
