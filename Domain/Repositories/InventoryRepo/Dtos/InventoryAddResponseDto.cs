namespace Library.Domain.Repositories.InventoryRepo.Dtos;

public class InventoryAddResponseDto
{
    public Guid Id { get; set; }
    public string BookName { get; set; }
    public int Amount { get; set; }
}
