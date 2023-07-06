using DAL.Repositories;
using Domain.Employee.Models;

namespace DAL.Repositories.Client;

public interface IEmployeeRepository : IBaseRepository<Domain.Employee.Models.Employee>
{

}