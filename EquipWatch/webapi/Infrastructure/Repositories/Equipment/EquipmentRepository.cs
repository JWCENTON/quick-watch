using System.Collections.Generic;
using DAL;
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
