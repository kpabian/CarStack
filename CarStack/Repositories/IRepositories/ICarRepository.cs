using CarStack.Models;
using System.Linq.Expressions;

namespace CarStack.Repositories.IRepositories;

public interface ICarRepository
{
    Task<IEnumerable<Car>> Get(int? carId = null);
    Task<IEnumerable<Car>> GetByFilter(Expression<Func<Car, bool>> filter);
    Task Create(Car car);
    Task Update(Car car);
    Task Delete(int carId);
}
