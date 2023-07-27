using Domain.BookedEquipment.Models;
using Domain.CheckIn.Models;
using Domain.CheckOut.Models;
using Domain.Client.Models;
using Domain.Commission.Models.Commission;
using Domain.Company.Models;
using Domain.Employee;
using Domain.Employee.Models;
using Domain.Equipment.Models;
using Domain.Invite;
using Domain.Invite.Models;
using Domain.WorksOn.Models;

namespace DAL;

public class Seed
{
    public static void IfDbEmptyAddNewItems(DatabaseContext context, IdentityContext identityContext)
    {

        if (!context.Company.Any() && identityContext.Users.Any())
        {
            var Company1 = new Company
            {
                Id = Guid.NewGuid(),
                Name = "FogWizards",
                OwnerId = identityContext.Users.First().Id
            };
            //var Company2 = new Company
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "someasdasdthing",
            //    OwnerId = identityContext.Users.First().Id
            //};
            context.Company.Add(Company1);
            //context.Company.Add(Company2);

            //var employee1 = new Employee()
            //{
            //    Company = Company1,
            //    CompanyId = Company1.Id,
            //    UserId = identityContext.Users.First().Id,
            //    Id = Guid.NewGuid(),
            //    Role = Role.Engineer
            //};
            //var employee2 = new Employee()
            //{
            //    Company = Company2,
            //    CompanyId = Company2.Id,
            //    UserId = identityContext.Users.First().Id,
            //    Id = Guid.NewGuid(),
            //    Role = Role.Engineer
            //};
            //context.Employees.Add(employee1);
            //context.Employees.Add(employee2);

            var equipment1 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "4725375345",
                Category = "Fog machine",
                Location = "Storage Room 3",
                Condition = 4,
                IsCheckedOut = false
            };
            var equipment2 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "7638263963",
                Category = "Fog machine",
                Location = "Event Center",
                Condition = 3,
                IsCheckedOut = true
            };
            var equipment3 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "6253791364",
                Category = "Fog machine",
                Location = "Stage B",
                Condition = 5,
                IsCheckedOut = true
            };
            var equipment4 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "7462684743",
                Category = "Fog machine",
                Location = "Warehouse 2",
                Condition = 1,
                IsCheckedOut = false
            };

            var checkIn1 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment1,
                EquipmentId = equipment1.Id,
                Id = Guid.NewGuid(),
                Time = DateTime.Now

            };
            var checkIn2 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment2,
                EquipmentId = equipment2.Id,
                Id = Guid.NewGuid(),
                Time = DateTime.Now

            };

            var checkOut1 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment1,
                EquipmentId = equipment1.Id,
                Id = Guid.NewGuid(),
                Time = DateTime.Now

            };
            var checkOut2 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment2,
                EquipmentId = equipment2.Id,
                Id = Guid.NewGuid(),
                Time = DateTime.Now
            };
            context.CheckIns.Add(checkIn1);
            context.CheckIns.Add(checkIn2);
            context.CheckOuts.Add(checkOut1);
            context.CheckOuts.Add(checkOut2);

            equipment1.IsCheckedOut = true;
            equipment2.IsCheckedOut = true;

            var Client1 = new Client
            {
                Company = Company1,
                CompanyId = Company1.Id,
                Email = "monika.wojciechowska@example.com",
                FirstName = "Monika",
                Id = Guid.NewGuid(),
                LastName = "Wojciechowska",
                PhoneNumber = "686745321",
                ContactAddress = "ul. Mickiewicza 12, Wrocław 50-001, Poland"
            };
            var Client2 = new Client
            {
                Company = Company1,
                CompanyId = Company1.Id,
                Email = "piotr.nowak@example.net",
                FirstName = "Piotr",
                Id = Guid.NewGuid(),
                LastName = "Nowak",
                PhoneNumber = "734512890",
                ContactAddress = "ul. Wielka 7, Kraków 30-001, Poland"
            };
            var Client3 = new Client
            {
                Company = Company1,
                CompanyId = Company1.Id,
                Email = "anna.kowalska@example.com",
                FirstName = "Anna",
                Id = Guid.NewGuid(),
                LastName = "Kowalska",
                PhoneNumber = "512345678",
                ContactAddress = "ul. Nowa 15, Warszawa 02-123, Poland"
            };
            var Client4 = new Client
            {
                Company = Company1,
                CompanyId = Company1.Id,
                Email = "adam.mazur@example.net",
                FirstName = "Adam",
                Id = Guid.NewGuid(),
                LastName = "Mazur",
                PhoneNumber = "667823409",
                ContactAddress = "ul. Leśna 3, Poznań 61-001, Poland"
            };
            var Client5 = new Client
            {
                Company = Company1,
                CompanyId = Company1.Id,
                Email = "katarzyna.wisniewska@example.com",
                FirstName = "Katarzyna",
                Id = Guid.NewGuid(),
                LastName = "Wiśniewska",
                PhoneNumber = "567809234",
                ContactAddress = "ul. Słoneczna 9, Gdańsk 80-001, Poland"
            };
            context.Client.Add(Client1);
            context.Client.Add(Client2);
            context.Client.Add(Client3);
            context.Client.Add(Client4);
            context.Client.Add(Client5);

            var commission1 = new Commission()
            {
                Client = Client1,
                ClientId = Client1.Id,
                Company = Company1,
                CompanyId = Company1.Id,
                Description = "Outdoor Halloween Party",
                EndTime = DateTime.Now.AddDays(3),
                Id = Guid.NewGuid(),
                Location = "Central Park, New York City",
                Scope = "Fog machine rental and setup for spooky ambiance",
                StartTime = DateTime.Now.AddDays(1)
            };
            var commission2 = new Commission()
            {
                Client = Client2,
                ClientId = Client2.Id,
                Company = Company1,
                CompanyId = Company1.Id,
                Description = "Indoor Theatrical Production",
                EndTime = DateTime.Now.AddDays(16),
                Id = Guid.NewGuid(),
                Location = "City Theatre, 123 Main Street",
                Scope = "Fog machine rental for special effects during the play",
                StartTime = DateTime.Now.AddDays(10)
            };
            context.Commissions.Add(commission1);
            context.Commissions.Add(commission2);
            equipment1.Company = Company1;
            equipment2.Company = Company1;
            equipment3.Company = Company1;
            equipment4.Company = Company1;
            context.Equipment.Add(equipment1);
            context.Equipment.Add(equipment2);
            context.Equipment.Add(equipment3);
            context.Equipment.Add(equipment4);

            var book1 = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                Commission = commission1,
                CommissionId = commission1.Id,
                Equipment = equipment1,
                EquipmentId = equipment1.Id
            };
            var book2 = new BookedEquipment()
            {
                Commission = commission2,
                CommissionId = commission2.Id,
                Equipment = equipment2,
                EquipmentId = equipment2.Id
            };
            context.BookedEquipments.Add(book1);
            context.BookedEquipments.Add(book2);

            var work1 = new WorksOn()
            {
                Id = Guid.NewGuid(),
                Commission = commission1,
                CommissionId = commission1.Id,
                UserId = identityContext.Users.First().Id
            };
            var work2 = new WorksOn()
            {
                Id = Guid.NewGuid(),
                Commission = commission2,
                CommissionId = commission2.Id,
                UserId = identityContext.Users.First().Id
            };
            context.WorksOn.Add(work1);
            context.WorksOn.Add(work2);
            var invite1 = new Invite()
            {
                UserId = identityContext.Users.First().Id,
                Company = Company1,
                CompanyId = Company1.Id,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Status = Status.Sent
            };
            var invite2 = new Invite()
            {
                UserId = identityContext.Users.First().Id,
                Company = Company1,
                CompanyId = Company1.Id,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Status = Status.Sent
            };
            context.Invites.Add(invite1);
            context.Invites.Add(invite2);


            context.SaveChanges();
        }

    }
}