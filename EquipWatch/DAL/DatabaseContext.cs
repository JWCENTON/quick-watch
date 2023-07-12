
using Domain.BookedEquipment.Models;
using Domain.CheckIn.Models;
using Domain.CheckOut.Models;
using Domain.Client.Models;
using Domain.Commission.Models.Commission;
using Domain.Company.Models;
using Domain.Employee.Models;
using Microsoft.EntityFrameworkCore;
using Domain.User.Models;
using Domain.Equipment.Models;
using Domain.Invite.Models;
using Domain.WorksOn.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Emit;
using Domain.Employee;
using Domain.Invite;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Commission>()
            .HasOne(c => c.Company)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<WorksOn>()
            .HasOne(w => w.Employee)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        //builder.Entity<Company>()
        //    .HasOne(c => c.Owner)
        //    .WithMany()
        //    .OnDelete(DeleteBehavior.SetNull);

        //builder.Entity<Employee>()
        //    .HasOne(e => e.User)
        //    .WithMany()
        //    .OnDelete(DeleteBehavior.SetNull);

        //builder.Entity<Invite>()
        //    .HasOne(i => i.User)
        //    .WithMany()
        //    .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<BookedEquipment>()
            .HasOne(b => b.Equipment)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CheckIn>()
            .HasOne(c => c.Equipment)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<CheckOut>()
            .HasOne(c => c.Equipment)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }


    public static void IfDbEmptyAddNewItems(DatabaseContext context)
    {
        if (!context.Equipment.Any())
        {
            var User1 = new User()
            {
                FirstName = "user",
                LastName = "123"
            };
            var User2 = new User()
            {
                FirstName = "resu",
                LastName = "312"
            };

            var Company1 = new Company
            {
                Id = new Guid(),
                Name = "something",
                Owner = User1
            };
            var Company2 = new Company
            {
                Id = new Guid(),
                Name = "someasdasdthing",
                Owner = User2
            };
            context.Company.Add(Company1);
            context.Company.Add(Company2);

            var employee1 = new Employee()
            {
                Company = Company1,
                Id = new Guid(),
                User = User1,
                Role = Role.Engineer
            };
            var employee2 = new Employee()
            {
                Company = Company2,
                Id = new Guid(),
                User = User2,
                Role = Role.Engineer
            };
            context.Employees.Add(employee1);
            context.Employees.Add(employee2);

            var equipment1 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "123",
                Category = "Fog machine",
                Location = "my house",
                Condition = 4,
                IsCheckedOut = false
            };
            var equipment2 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "133",
                Category = "Fog machine",
                Location = "my house",
                Condition = 3,
                IsCheckedOut = false
            };
            var equipment3 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "14235",
                Category = "Fog machine",
                Location = "my house",
                Condition = 5,
                IsCheckedOut = true
            };
            var equipment4 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "11234",
                Category = "Fog machine",
                Location = "my house",
                Condition = 1,
                IsCheckedOut = false
            };

            var checkIn1 = new CheckIn()
            {
                Employee = employee1,
                Equipment = equipment1,
                Id = new Guid(),
                Time = DateTime.Now

            };
            var checkIn2 = new CheckIn()
            {
                Employee = employee2,
                Equipment = equipment2,
                Id = new Guid(),
                Time = DateTime.Now

            };

            var checkOut1 = new CheckOut()
            {
                Employee = employee1,
                Equipment = equipment1,
                Id = new Guid(),
                Time = DateTime.Now

            };
            var checkOut2 = new CheckOut()
            {
                Employee = employee2,
                Equipment = equipment2,
                Id = new Guid(),
                Time = DateTime.Now
            };
            context.CheckIns.Add(checkIn1);
            context.CheckIns.Add(checkIn2);
            context.CheckOuts.Add(checkOut1);
            context.CheckOuts.Add(checkOut2);

            equipment1.CheckedOutBy = employee1;
            equipment1.IsCheckedOut = true;
            equipment2.CheckedOutBy = employee2;
            equipment2.IsCheckedOut = true;

            var Client1 = new Client
            {
                Company = Company1,
                Email = "some@some.com",
                FirstName = "someone",
                Id = new Guid(),
                LastName = "some",
                PhoneNumber = "+43234232423",
                ContactAddress = "ABBA"
            };
            var Client2 = new Client
            {
                Company = Company2,
                Email = "some@somasdasde.com",
                FirstName = "soasdasmeone",
                Id = new Guid(),
                LastName = "soasdasdme",
                PhoneNumber = "+43234344232423",
                ContactAddress = "BABA"
            };
            context.Client.Add(Client1);
            context.Client.Add(Client2);
            
            var commission1 = new Commission()
            {
                Client = Client1,
                Company = Company1,
                Description = "description",
                EndTime = DateTime.Now,
                Id = new Guid(),
                Location = "location",
                Scope = "scope",
                StartTime = DateTime.Now
            };
            var commission2 = new Commission()
            {
                Client = Client2,
                Company = Company2,
                Description = "description2",
                EndTime = DateTime.Now,
                Id = new Guid(),
                Location = "location2",
                Scope = "scope2",
                StartTime = DateTime.Now
            };
            context.Commissions.Add(commission1);
            context.Commissions.Add(commission2);
            equipment1.Commission = commission1;
            equipment2.Commission = commission2;
            equipment1.Company = Company1;
            equipment2.Company = Company2;
            equipment3.Company = Company2;
            equipment4.Company = Company1;
            context.Equipment.Add(equipment1);
            context.Equipment.Add(equipment2);
            context.Equipment.Add(equipment3);
            context.Equipment.Add(equipment4);

            var book1 = new BookedEquipment()
            {
                Id = new Guid(),
                Commission = commission1,
                Equipment = equipment1
            };
            var book2 = new BookedEquipment()
            {
                Commission = commission2,
                Equipment = equipment2
            };
            context.BookedEquipments.Add(book1);
            context.BookedEquipments.Add(book2);

            var work1 = new WorksOn()
            {
                Id = new Guid(),
                Commission = commission1,
                Employee = employee1
            };
            var work2 = new WorksOn()
            {
                Id = new Guid(),
                Commission = commission2,
                Employee = employee2
            };
            context.WorksOn.Add(work1);
            context.WorksOn.Add(work2);
            var invite1 = new Invite()
            {
                Company = Company1,
                Id = new Guid(),
                User = User1,
                CreatedAt = DateTime.Now,
                Status = Status.Sent
            };
            var invite2 = new Invite()
            {
                Company = Company2,
                Id = new Guid(),
                User = User2,
                CreatedAt = DateTime.Now,
                Status = Status.Sent
            };
            context.Invites.Add(invite1);
            context.Invites.Add(invite2);
            

            context.SaveChanges();
        }

    }
}

        
