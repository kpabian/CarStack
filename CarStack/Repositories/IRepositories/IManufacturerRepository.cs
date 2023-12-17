using CarStack.Models;

namespace CarStack.Repositories.IRepositories;

public interface IManufacturerRepository
{
    Task<IEnumerable<Manufacturer>> Get(int? manufacturerId);
    Task Create(Manufacturer car);
    Task Update(Manufacturer car);
    Task Delete(int manufacturerId);
}
