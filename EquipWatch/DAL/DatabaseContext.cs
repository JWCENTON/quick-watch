
using Domain.Client.Models;
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
    public DbSet<Client> Client { get; set; }

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

    public static void IfDbEmptyAddNewItems(DatabaseContext context)
    {
        if (!context.Equipment.Any())
        {
            // var test = new Equipment()
            //{
            //    Id = Guid.NewGuid(),
            //    SerialNumber = "123", 
            //    Category = "Fog machine", 
            //    Location = "my house", 
            //    Condition = 4, 
            //    IsCheckedOut = false
            //});
            var equipment1 = new Equipment(Guid.NewGuid(), "123", "Fog machine", "my house", 4, false);
             var equipment2 = new Equipment(Guid.NewGuid(), "133", "Fog machine", "my house", 3, false);
             var equipment3 = new Equipment(Guid.NewGuid(), "14235", "Fog machine", "my house", 5, true);
             var equipment4 = new Equipment(Guid.NewGuid(), "11234", "Fog machine", "my house", 1, false);
            context.Equipment.Add(equipment1);
            context.Equipment.Add(equipment2);
            context.Equipment.Add(equipment3);
            context.Equipment.Add(equipment4);

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
            context.Users.Add(User1);
            context.Users.Add(User2);

            var Company1 = new Company()
            {
                Id = new Guid(),
                Name = "something",
                Owner = User1
            };
            var Company2 = new Company()
            {
                Id = new Guid(),
                Name = "someasdasdthing",
                Owner = User2
            };
            context.Company.Add(Company1);
            context.Company.Add(Company2);


            var Client1 = new Client()
            {
                Company = Company1,
                Email = "some@some.com",
                FirstName = "someone",
                Id = new Guid(),
                LastName = "some",
                PhoneNumber = "+43234232423"
            };
            var Client2 = new Client()
            {
                Company = Company2,
                Email = "some@somasdasde.com",
                FirstName = "soasdasmeone",
                Id = new Guid(),
                LastName = "soasdasdme",
                PhoneNumber = "+43234344232423"
            };
            context.Client.Add(Client1);
            context.Client.Add(Client2);

            context.SaveChanges();
        }

    }
}

        
