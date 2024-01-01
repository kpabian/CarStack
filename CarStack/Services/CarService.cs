using CarStack.Models;
using CarStack.Repositories.IRepositories;
using CarStack.Services.IServices;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public sealed class CarService(ICarRepository _repo) : ICarService
{
    public async Task<IEnumerable<Car>> GetAll() => await _repo.Get();

    public async Task<Car?> GetById(int id) => (await _repo.Get(id)).FirstOrDefault();

    public async Task Create(Car car) => await _repo.Create(car);

    public async Task Update(Car car) => await _repo.Update(car);

    public async Task Delete(int carId) => await _repo.Delete(carId);
}
