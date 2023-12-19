using Domain.EquipmentInUse.Models;
using webapi.uow;

namespace webapi.Services;

public class EmployeeServices : IEmployeeServices
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<EquipmentInUse>> GetEquipmentInUseByUserIdAsync(string userId)
    {
        var allEquipment = await _unitOfWork.EquipmentInUse.GetAllAsync();
        return allEquipment.FindAll(equipment => equipment.UserId == userId);
    }
}