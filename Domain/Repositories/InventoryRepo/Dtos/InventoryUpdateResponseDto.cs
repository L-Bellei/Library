namespace Library.Domain.Repositories.InventoryRepo.Dtos;

public class InventoryUpdateResponseDto
{
    public Guid Id { get; set; }
    public string BookName { get; set; }
    public int Amount { get; set; }
}
