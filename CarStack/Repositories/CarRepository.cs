using CarStack.Areas.Identity.Data;
using CarStack.Models;
using CarStack.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarStack.Repositories;

public sealed class CarRepository(CarDBContext context) : ICarRepository
{
    private readonly CarDBContext _context = context;

    public async Task<IEnumerable<Car>> Get(int? carId = null)
    {
        var query = _context.Cars
            .Include(c => c.Manufacturer)
            .AsQueryable();
        if (carId != null)
            query = query.Where(x => x.Id == carId);
        return await query.ToListAsync();
    }

    public async Task Create(Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Car car)
    {
        var existingCar = await _context.Cars.FindAsync(car.Id);
        if (existingCar is null)
            throw new InvalidOperationException($"Samochód o ID {car.Id} nie został znaleziony.");
        
        _context.Entry(existingCar).CurrentValues.SetValues(car);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int carId)
    {
        var carToDelete = await _context.Cars.FindAsync(carId);
        if (carToDelete is null)
            throw new InvalidOperationException($"Samochód o ID {carId} nie został znaleziony.");

        _context.Cars.Remove(carToDelete);
        await _context.SaveChangesAsync();
    }
}
