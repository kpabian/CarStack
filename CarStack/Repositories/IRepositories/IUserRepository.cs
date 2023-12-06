using CarStack.Areas.Identity.Data;

namespace CarStack.Repositories.IRepositories;

public interface IUserRepository
{
    Task<IEnumerable<CarStackUser>> Get(int? userId);
    Task Create(CarStackUser car);
    Task Update(CarStackUser car);
    Task Delete(int userId);
}
