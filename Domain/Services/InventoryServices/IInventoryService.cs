using Library.Domain.Repositories.InventoryRepo.Dtos;

namespace Library.Domain.Services.InventoryServices;

public interface IInventoryService
{
    Task<InventoryGetResponseDto> GetInventoryAsync(Guid id);
    Task<InventoryAddResponseDto> AddInventoryAsync(InventoryAddRequestDto inventory);
    Task<IEnumerable<InventoryGetResponseDto>> GetAllInventoryAsync();
    Task<InventoryUpdateResponseDto> UpdateInventoryAsync(Guid id, InventoryUpdateRequestDto inventory);
    Task DeleteInventoryAsync(Guid id);
}
