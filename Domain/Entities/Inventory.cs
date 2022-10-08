namespace Library.Domain.Entities;

public class Inventory
{
    public Guid Id { get; set; }
    public int Amount {  get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }

    public Inventory()
    {
    }

    public Inventory(int amount, Guid bookId)
    {
        this.Id = new Guid();
        this.Amount = amount;
    }
}
