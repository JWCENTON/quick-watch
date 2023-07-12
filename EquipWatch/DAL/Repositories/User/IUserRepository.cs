using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.User;

public interface IUserRepository : IBaseRepository<Domain.User.Models.User>
{
}