using System.Collections.Generic;
using DAL;
using DAL.SQLData;
using Microsoft.EntityFrameworkCore;

namespace webapi.Infrastructure.Repositories.Equipment
{
    public class EquipmentRepository<Equipment> : IEquipmentRepository<Equipment>
    {
        private readonly EquipmentDbContext _context;

        public EquipmentRepository(EquipmentDbContext context)
        {
            _context = context;
        }
    }
}
