using CarStack.Models;
using CarStack.Repositories.IRepositories;
using CarStack.Services.IServices;

namespace Infrastructure.Services;

public sealed class ManufacturerService(IManufacturerRepository _repo) : IManufacturerService
{
    public async Task<IEnumerable<Manufacturer>> GetAll() => await _repo.Get(null);

    public async Task<Manufacturer?> GetById(int id) => (await _repo.Get(id)).FirstOrDefault();

    public async Task Create(Manufacturer manufacturer) => await _repo.Create(manufacturer);

    public async Task Update(Manufacturer manufacturer) => await _repo.Update(manufacturer);

    public async Task Delete(int manufacturerId) => await _repo.Delete(manufacturerId);
}

