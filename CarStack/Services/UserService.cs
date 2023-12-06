using CarStack.Areas.Identity.Data;
using CarStack.Models;
using CarStack.Repositories.IRepositories;
using CarStack.Services.IServices;

namespace Infrastructure.Services;

public sealed class UserService(IUserRepository _repo) : IUserService
{
    public async Task<IEnumerable<CarStackUser>> GetAll() => await _repo.Get(null);

    public async Task<CarStackUser?> GetById(int id) => (await _repo.Get(id)).FirstOrDefault();

    public async Task Create(CarStackUser user) => await _repo.Create(user);

    public async Task Update(CarStackUser user) => await _repo.Update(user);

    public async Task Delete(int userId) => await _repo.Delete(userId);
}
