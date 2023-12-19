using Domain.EquipmentInUse.Models;

namespace webapi.Services;

public interface IEmployeeServices
{
    Task<List<EquipmentInUse>> GetEquipmentInUseByUserIdAsync(string userId);
}