using CarStack.Models;

namespace CarStack.Services.IServices;

public interface IManufacturerService
{
    Task<IEnumerable<Manufacturer>> GetAll();
    Task<Manufacturer?> GetById(int id);
    Task Create(Manufacturer manufacturer);
    Task Update(Manufacturer manufacturer);
    Task Delete(int id);
}
