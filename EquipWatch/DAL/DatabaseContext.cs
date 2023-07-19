using Domain.BookedEquipment.Models;
using Domain.CheckIn.Models;
using Domain.CheckOut.Models;
using Domain.Client.Models;
using Domain.Commission.Models.Commission;
using Domain.Company.Models;
using Domain.Employee.Models;
using Domain.Equipment.Models;
using Domain.Invite.Models;
using Domain.WorksOn.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class DatabaseContext : DbContext
{
    public DbSet<BookedEquipment> BookedEquipments { get; set; }
    public DbSet<CheckIn> CheckIns { get; set; }
    public DbSet<CheckOut> CheckOuts { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<Commission> Commissions { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<WorksOn> WorksOn { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BookedEquipment>()
            .HasOne(b => b.Equipment)
            .WithMany()
            .HasForeignKey("EquipmentId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<BookedEquipment>()
            .HasOne(b => b.Commission)
            .WithMany()
            .HasForeignKey("CommissionId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CheckIn>()
            .HasOne(c => c.Equipment)
            .WithMany()
            .HasForeignKey("EquipmentId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CheckIn>()
            .HasOne(c => c.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CheckOut>()
            .HasOne(c => c.Equipment)
            .WithMany()
            .HasForeignKey("EquipmentId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CheckOut>()
            .HasOne(c => c.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<WorksOn>()
            .HasOne(w => w.Commission)
            .WithMany()
            .HasForeignKey("CommissionId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<WorksOn>()
            .HasOne(w => w.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Client>()
            .HasOne(c => c.Company)
            .WithMany()
            .HasForeignKey("CompanyId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Commission>()
            .HasOne(c => c.Client)
            .WithMany()
            .HasForeignKey("ClientId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Commission>()
            .HasOne(c => c.Company)
            .WithMany()
            .HasForeignKey("CompanyId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Invite>()
            .HasOne(i => i.Company)
            .WithMany()
            .HasForeignKey("CompanyId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Equipment>()
            .HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey("CompanyId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Employee>()
            .HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey("CompanyId")
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

}

