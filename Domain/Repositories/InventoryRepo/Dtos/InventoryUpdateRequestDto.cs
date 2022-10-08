namespace Library.Domain.Repositories.InventoryRepo.Dtos;

public class InventoryUpdateRequestDto
{
    public Guid BookId { get; set; }
    public int Amount { get; set; }
}
