using CarStack.Areas.Identity.Data;
using CarStack.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CarStack.Repositories;

public sealed class UserRepository(CarDBContext context) : IUserRepository
{
    private readonly CarDBContext _context = context;

    public async Task<IEnumerable<CarStackUser>> Get(int? userId)
    {
        var query = _context.Users.AsQueryable();
        if (userId != null)
            query = query.Where(x => x.Id == userId);

        return await query.ToListAsync();
    }

    public async Task Create(CarStackUser user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(CarStackUser user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        _context.Entry(existingUser).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int userId)
    {
        var userToDelete = await _context.Users.FindAsync(userId);
        _context.Users.Remove(userToDelete);
        await _context.SaveChangesAsync();
    }
}
