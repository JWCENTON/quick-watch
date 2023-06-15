using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.SQLData
{
    public class EquipmentDbContext : DbContext
    {
        public DbSet<Equipment> Equipments { get; set; }

        public EquipmentDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
