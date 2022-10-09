namespace Library.Domain.Entities;

public class Movimentation
{
    public Guid Id { get; set; }
    public Book Book { get; set; }
    public Guid BookId { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public DateTime MovimentationDate { get; set; }

    public Movimentation()
    {
    }
}
