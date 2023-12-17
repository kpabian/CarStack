using CarStack.Areas.Identity.Data;
using CarStack.Models;
using CarStack.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CarStack.Repositories;

public sealed class ManufacturerRepository(CarDBContext context) : IManufacturerRepository
{
    private readonly CarDBContext _context = context;

    public async Task<IEnumerable<Manufacturer>> Get(int? manufacturerId)
    {
        var query = _context.Manufacturers.AsQueryable();
        if (manufacturerId != null)
            query = query.Where(x => x.Id == manufacturerId);
        return await query.ToListAsync();
    }

    public async Task Create(Manufacturer manufacturer)
    {
        await _context.Manufacturers.AddAsync(manufacturer);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Manufacturer manufacturer)
    {
        var existingManufacturer = await _context.Manufacturers.FindAsync(manufacturer.Id);
        _context.Entry(existingManufacturer).CurrentValues.SetValues(manufacturer);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int manufacturerId)
    {
        var manufacturerToDelete = await _context.Manufacturers.FindAsync(manufacturerId);
        _context.Manufacturers.Remove(manufacturerToDelete);
        await _context.SaveChangesAsync();
    }
}
