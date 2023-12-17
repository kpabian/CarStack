using CarStack.Models;
using System.Linq.Expressions;

namespace CarStack.Repositories.IRepositories;

public interface ICarRepository
{
    Task<IEnumerable<Car>> Get(int? carId = null);
    Task Create(Car car);
    Task Update(Car car);
    Task Delete(int carId);
}
