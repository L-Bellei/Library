using Library.Domain.Repositories.InventoryRepo.Dtos;

namespace Library.Domain.Services.InventoryServices;

public interface IInventoryService
{
    Task<InventoryGetResponseDto> GetInventoryAsync(Guid id);
    Task<InventoryAddResponseDto> AddInventoryAsync(string username, InventoryAddRequestDto inventory);
    Task<IEnumerable<InventoryGetResponseDto>> GetAllInventoryAsync();
    Task<InventoryUpdateResponseDto> UpdateInventoryAsync(Guid id, string username, InventoryUpdateRequestDto inventory);
    Task DeleteInventoryAsync(Guid id);
}
