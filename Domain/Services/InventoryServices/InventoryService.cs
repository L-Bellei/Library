using Library.Domain.Entities;
using Library.Domain.Repositories.BookRepo.Dtos;
using Library.Domain.Repositories.InventoryRepo;
using Library.Domain.Repositories.InventoryRepo.Dtos;
using Library.Domain.Services.BookServices;

namespace Library.Domain.Services.InventoryServices;

public class InventoryService : IInventoryService
{
    private readonly IBookService bookService;
    private readonly IInventoryRepository inventoryRepository;

    public InventoryService(IBookService bookService, IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
        this.bookService = bookService;
    }

    public async Task<InventoryAddResponseDto> AddInventoryAsync(InventoryAddRequestDto inventory)
    {
        BookGetResponseDto book = await bookService.GetBookByIdAsync(inventory.BookId);
        Inventory? inventoryExists = await inventoryRepository.GetInventoryByBookIdAsync(book.Id);

        if (inventoryExists != null)
            throw new Exception("This book's already registered on Inventory");
        else
        {
            Inventory inventoryAdded = await inventoryRepository.AddInventoryAsync(new Inventory
            {
                BookId = book.Id,
                Amount = inventory.Amount,
            });

            return new InventoryAddResponseDto
            {
                Id = inventoryAdded.Id,
                BookName = book.Title,
                Amount = inventoryAdded.Amount,
            };
        }
    }

    public async Task DeleteInventoryAsync(Guid id)
    {
        await inventoryRepository.DeleteInventoryAsync(id);
    }

    public async Task<IEnumerable<InventoryGetResponseDto>> GetAllInventoryAsync()
    {
        IEnumerable<Inventory>? inventories = await inventoryRepository.GetAllInventoryAsync();
        IList<InventoryGetResponseDto> inventoryRegisters = new List<InventoryGetResponseDto>();

        if (inventories != null)
        {
            foreach(var inv in inventories)
            {
                BookGetResponseDto book = await bookService.GetBookByIdAsync(inv.BookId);
                InventoryGetResponseDto aux = new()
                {
                    Id = inv.Id,
                    BookName = book.Title,
                    Amount = inv.Amount,
                };

                inventoryRegisters.Add(aux);
            }

            return inventoryRegisters;
        }
        else
            throw new Exception("No registers added yet");
    }

    public async Task<InventoryGetResponseDto> GetInventoryAsync(Guid id)
    {
        Inventory? inventory = await inventoryRepository.GetInventoryByIdAsync(id);

        if (inventory == null)
            throw new Exception("Register not found");
        else
        {
            BookGetResponseDto book = await bookService.GetBookByIdAsync(inventory.BookId);

            return new InventoryGetResponseDto
            {
                Id = inventory.Id,
                BookName = book.Title,
                Amount = inventory.Amount,
            };
        }

    }

    public async Task<InventoryUpdateResponseDto> UpdateInventoryAsync(Guid id, InventoryUpdateRequestDto inventory)
    {
        Inventory? inventoryFinded = await inventoryRepository.GetInventoryByIdAsync(id);
        BookGetResponseDto book = await bookService.GetBookByIdAsync(inventory.BookId);

        if (inventoryFinded == null)
            throw new Exception("Register not found");

        inventoryFinded.Amount = inventory.Amount;

        Inventory inventoryUpdated = await inventoryRepository.UpdateInventoryAsync(inventoryFinded);

        return new InventoryUpdateResponseDto
        {
            Id = inventoryUpdated.Id,
            BookName = book.Title,
            Amount = inventoryUpdated.Amount,
        };
    }
}
