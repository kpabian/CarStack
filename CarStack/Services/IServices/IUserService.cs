using CarStack.Areas.Identity.Data;

namespace CarStack.Services.IServices;

public interface IUserService
{
    Task<IEnumerable<CarStackUser>> GetAll();
    Task<CarStackUser?> GetById(int id);
    Task Create(CarStackUser user);
    Task Update(CarStackUser user);
    Task Delete(int id);
}
