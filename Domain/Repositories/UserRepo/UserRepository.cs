using Library.Domain.Entities;
using Library.Infra;
using Microsoft.EntityFrameworkCore;

namespace Library.Domain.Repositories.UserRepo;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext db;

    public UserRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task<User> AddUserAsync(User user)
    {
        if (!(db.Users.Include(x => x.Email == user.Email) != null))
            throw new Exception("E-mail's already registered");

        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        return user;
    }

    public async Task<User> UpdateUserAsync(Guid id, User user)
    {
        User? userFinded = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
    
        if(userFinded == null)
            throw new Exception("User not found");

        if (!(userFinded.Email == user.Email))
        {
            if (!(db.Users.Include(x => x.Email == user.Email) != null))
                throw new Exception("E-mail's already registered");
        }

        userFinded.UserName = user.UserName;
        userFinded.Email = user.Email;
        userFinded.Cpf = user.Cpf;
        userFinded.Role = user.Role;

        db.Users.Update(userFinded);
        await db.SaveChangesAsync();

        return userFinded;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        User? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            throw new Exception("User not found");

        db.Users.Remove(user);
        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>?> GetAllUsersAsync()
    {
        IEnumerable<User> users = await db.Users.ToListAsync();

        if (users.Count() == 0)
            return null;

        return users;
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        User? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            throw new Exception("User not Found");

        return user;
    }
}
