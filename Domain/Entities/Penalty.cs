namespace Library.Domain.Entities;

public class Penalty
{
    public Guid Id { get; set; }
    public float PenaltyPrice { get; set; }
    public bool Settled { get; set; }
    public Book Book { get; set; }
    public Guid BookId { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }

    public Penalty()
    {
    }

    public Penalty(float penaltyPrice, bool settled)
    {
        this.Id = new Guid();
        this.PenaltyPrice = penaltyPrice;
        this.Settled = settled;
    }
}
