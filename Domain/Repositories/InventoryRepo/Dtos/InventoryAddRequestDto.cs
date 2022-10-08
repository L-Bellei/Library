namespace Library.Domain.Repositories.InventoryRepo.Dtos;

public class InventoryAddRequestDto
{
    public Guid BookId { get; set; }
    public int Amount { get; set; }
}
