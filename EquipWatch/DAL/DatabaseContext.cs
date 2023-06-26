
using Domain.Company.Models;
using Microsoft.EntityFrameworkCore;
using Domain.User.Models;
using Domain.Equipment.Models;

namespace DAL;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Company> Company { get; set; }
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
