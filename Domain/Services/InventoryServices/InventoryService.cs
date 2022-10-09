using Library.Domain.Entities;
using Library.Domain.Repositories.BookRepo;
using Library.Domain.Repositories.InventoryRepo;
using Library.Domain.Repositories.InventoryRepo.Dtos;
using Library.Domain.Repositories.MovimentationRepo;
using Library.Domain.Repositories.UserRepo;

namespace Library.Domain.Services.InventoryServices;

public class InventoryService : IInventoryService
{
    private readonly IBookRepository bookRepository;
    private readonly IInventoryRepository inventoryRepository;
    private readonly IMovimentationRepository movimentationRepository;
    private readonly IUserRepository userRepository;

    public InventoryService(IBookRepository bookRepository, IInventoryRepository inventoryRepository,
        IMovimentationRepository movimentationRepository, IUserRepository userRepository)
    {
        this.inventoryRepository = inventoryRepository;
        this.bookRepository = bookRepository;
        this.movimentationRepository = movimentationRepository;
        this.userRepository = userRepository;
    }

    public async Task<InventoryAddResponseDto> AddInventoryAsync(string username, InventoryAddRequestDto inventory)
    {
        Book book = await bookRepository.GetBookById(inventory.BookId);
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

            IEnumerable<User>? users = await userRepository.GetAllUsersAsync();
            User? user = users!.FirstOrDefault(u => u.UserName == username);

            Movimentation movimentationAdded = await movimentationRepository.AddMovimentationAsync(new Movimentation
            {
                BookId = book.Id,
                UserId = user!.Id,
            });

            if (movimentationAdded != null)
            {

                return new InventoryAddResponseDto
                {
                    Id = inventoryAdded.Id,
                    BookName = book.Title,
                    Amount = inventoryAdded.Amount,
                };
            }
            else
                throw new Exception("Unable to add to inventory");
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
            foreach (var inv in inventories)
            {
                Book book = await bookRepository.GetBookById(inv.BookId);
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
            Book book = await bookRepository.GetBookById(inventory.BookId);

            return new InventoryGetResponseDto
            {
                Id = inventory.Id,
                BookName = book.Title,
                Amount = inventory.Amount,
            };
        }

    }

    public async Task<InventoryUpdateResponseDto> UpdateInventoryAsync(Guid id, string username, InventoryUpdateRequestDto inventory)
    {
        Inventory? inventoryFinded = await inventoryRepository.GetInventoryByIdAsync(id);
        Book book = await bookRepository.GetBookById(inventory.BookId);

        if (inventoryFinded == null)
            throw new Exception("Register not found");

        inventoryFinded.Amount = inventory.Amount;

        Inventory inventoryUpdated = await inventoryRepository.UpdateInventoryAsync(inventoryFinded);

        IEnumerable<User>? users = await userRepository.GetAllUsersAsync();
        User? user = users!.FirstOrDefault(u => u.UserName == username);

        Movimentation movimentationAdded = await movimentationRepository.AddMovimentationAsync(new Movimentation
        {
            BookId = book.Id,
            UserId = user!.Id,
        });
        
        if (movimentationAdded != null)
        {
            return new InventoryUpdateResponseDto
            {
                Id = inventoryUpdated.Id,
                BookName = book.Title,
                Amount = inventoryUpdated.Amount,
            };

        }
        else
            throw new Exception("Unable to update inventory");
    }
}
