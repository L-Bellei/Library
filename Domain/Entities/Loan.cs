namespace Library.Domain.Entities;

public class Loan
{
    public Guid Id { get; set; }
    public DateTime LoanDate  { get; set; }
    public DateTime DeadlineDate  { get; set; }
    public DateTime DevolutionDate  { get; set; }
    public bool Returned { get; set; }
    public Book Book { get; set; }
    public Guid BookId { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public List<Penalty> Penalties { get; set; }

    public Loan()
    {
    }

    public Loan(DateTime loanDate, DateTime deadlineDate, bool returned)
    {
        this.Id = new Guid();
        this.LoanDate = loanDate;
        this.DeadlineDate = deadlineDate;
        this.Returned = returned;
    }
}
