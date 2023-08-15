using Domain.BookedEquipment.Models;
using Domain.CheckIn.Models;
using Domain.CheckOut.Models;
using Domain.Client.Models;
using Domain.Commission.Models.Commission;
using Domain.Company.Models;
using Domain.Equipment.Models;
using Domain.Invite;
using Domain.Invite.Models;
using Domain.Reservation.Models;
using Domain.WorksOn.Models;

namespace DAL;

public class Seed
{
    public static void IfDbEmptyAddNewItems(DatabaseContext context, IdentityContext identityContext)
    {

        if (!context.Company.Any() && identityContext.Users.Any())
        {
            var company1 = new Company
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
            context.Company.Add(company1);
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
                Available = true,
                InWarehouse = true
            };
            var equipment2 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "7638263963",
                Category = "Fog machine",
                Location = "Event Center",
                Condition = 3,
                Available = true,
                InWarehouse = true
            };
            var equipment3 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "6253791364",
                Category = "Fog machine",
                Location = "Stage B",
                Condition = 5,
                Available = true,
                InWarehouse = true
            };
            var equipment4 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "7462684743",
                Category = "Fog machine",
                Location = "Warehouse 2",
                Condition = 1,
                Available = true,
                InWarehouse = true
            };
            equipment1.Company = company1;
            equipment2.Company = company1;
            equipment3.Company = company1;
            equipment4.Company = company1;
            context.Equipment.Add(equipment1);
            context.Equipment.Add(equipment2);
            context.Equipment.Add(equipment3);
            context.Equipment.Add(equipment4);

            var client1 = new Client
            {
                Company = company1,
                CompanyId = company1.Id,
                Email = "monika.wojciechowska@example.com",
                FirstName = "Monika",
                Id = Guid.NewGuid(),
                LastName = "Wojciechowska",
                PhoneNumber = "686745321",
                ContactAddress = "ul. Mickiewicza 12, Wrocław 50-001, Poland"
            };
            var client2 = new Client
            {
                Company = company1,
                CompanyId = company1.Id,
                Email = "piotr.nowak@example.net",
                FirstName = "Piotr",
                Id = Guid.NewGuid(),
                LastName = "Nowak",
                PhoneNumber = "734512890",
                ContactAddress = "ul. Wielka 7, Kraków 30-001, Poland"
            };
            var client3 = new Client
            {
                Company = company1,
                CompanyId = company1.Id,
                Email = "anna.kowalska@example.com",
                FirstName = "Anna",
                Id = Guid.NewGuid(),
                LastName = "Kowalska",
                PhoneNumber = "512345678",
                ContactAddress = "ul. Nowa 15, Warszawa 02-123, Poland"
            };
            var client4 = new Client
            {
                Company = company1,
                CompanyId = company1.Id,
                Email = "adam.mazur@example.net",
                FirstName = "Adam",
                Id = Guid.NewGuid(),
                LastName = "Mazur",
                PhoneNumber = "667823409",
                ContactAddress = "ul. Leśna 3, Poznań 61-001, Poland"
            };
            var client5 = new Client
            {
                Company = company1,
                CompanyId = company1.Id,
                Email = "katarzyna.wisniewska@example.com",
                FirstName = "Katarzyna",
                Id = Guid.NewGuid(),
                LastName = "Wiśniewska",
                PhoneNumber = "567809234",
                ContactAddress = "ul. Słoneczna 9, Gdańsk 80-001, Poland"
            };
            context.Client.Add(client1);
            context.Client.Add(client2);
            context.Client.Add(client3);
            context.Client.Add(client4);
            context.Client.Add(client5);

            var commission1 = new Commission()
            {
                Client = client1,
                ClientId = client1.Id,
                Company = company1,
                CompanyId = company1.Id,
                Description = "Outdoor Halloween Party",
                Id = Guid.NewGuid(),
                Location = "Central Park, New York City",
                Scope = "Fog machine rental and setup for spooky ambiance",
                CreationTime = DateTime.Now,
                StartTime = DateTime.Now - TimeSpan.FromDays(25),
                EndTime = DateTime.Now.AddDays(20)
            };
            var commission2 = new Commission()
            {
                Client = client2,
                ClientId = client2.Id,
                Company = company1,
                CompanyId = company1.Id,
                Description = "Indoor Theatrical Production",
                Id = Guid.NewGuid(),
                Location = "City Theatre, 123 Main Street",
                Scope = "Fog machine rental for special effects during the play",
                CreationTime = DateTime.Now,
                StartTime = DateTime.Now - TimeSpan.FromDays(30)
            };
            context.Commissions.Add(commission1);
            context.Commissions.Add(commission2);

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


            var reservation1 = new Reservation
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment1,
                EquipmentId = equipment2.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now - TimeSpan.FromDays(30),
                StartDate = DateTime.Now - TimeSpan.FromDays(20),
                EndDate = DateTime.Now - TimeSpan.FromDays(10)
            };

            var reservation2 = new Reservation
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment2,
                EquipmentId = equipment2.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now - TimeSpan.FromDays(29),
                StartDate = DateTime.Now - TimeSpan.FromDays(27),
                EndDate = DateTime.Now - TimeSpan.FromDays(20),
            };
            context.Reservations.Add(reservation1);
            context.Reservations.Add(reservation2);

            var book1 = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                Commission = commission1,
                CommissionId = commission1.Id,
                Reservation = reservation1,
                ReservationId = reservation1.Id,
            };

            var book2 = new BookedEquipment()
            {
                Commission = commission2,
                CommissionId = commission2.Id,
                Reservation = reservation2,
                ReservationId = reservation2.Id,
            };

            context.BookedEquipments.Add(book1);
            context.BookedEquipments.Add(book2);


            var checkOut1 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment1,
                EquipmentId = equipment1.Id,
                Id = Guid.NewGuid(),
                CreationTime = reservation1.StartDate,
                EndTime = reservation1.EndDate
            };

            var checkOut2 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment2,
                EquipmentId = equipment2.Id,
                Id = Guid.NewGuid(),
                CreationTime = reservation2.StartDate,
                EndTime = reservation2.EndDate
            };

            context.CheckOuts.Add(checkOut1);
            book1.CheckOutId = checkOut1.Id;
            book1.CheckOut = checkOut1;
            checkOut1.Equipment.Available = false;
            checkOut1.Equipment.InWarehouse = false;
            checkOut1.Equipment.Location = book1.Commission.Location;
            checkOut1.ArriveTime = checkOut1.CreationTime.AddHours(12);

            context.CheckOuts.Add(checkOut2);
            book2.CheckOutId = checkOut2.Id;
            book2.CheckOut = checkOut2;
            checkOut2.Equipment.Available = false;
            checkOut2.Equipment.InWarehouse = false;
            checkOut2.Equipment.Location = book2.Commission.Location;
            checkOut2.ArriveTime = checkOut2.CreationTime.AddHours(5);

            var checkOut3 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment3,
                EquipmentId = equipment3.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now - TimeSpan.FromDays(20),
                EndTime = DateTime.Now - TimeSpan.FromDays(13)
            };

            var book3 = new BookedEquipment()
            {
                Commission = commission2,
                CommissionId = commission2.Id,
                CheckOut = checkOut3,
                CheckOutId = checkOut3.Id,
            };

            context.BookedEquipments.Add(book3);
            context.CheckOuts.Add(checkOut3);
            checkOut3.Equipment.Available = false;
            checkOut3.Equipment.InWarehouse = false;
            checkOut3.Equipment.Location = book3.Commission.Location;
            checkOut3.ArriveTime = checkOut3.CreationTime.AddHours(2);

            var checkOut4 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment4,
                EquipmentId = equipment4.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now - TimeSpan.FromDays(10)
            };

            var book4 = new BookedEquipment()
            {
                Commission = commission2,
                CommissionId = commission2.Id,
                CheckOut = checkOut4,
                CheckOutId = checkOut4.Id,
            };

            context.BookedEquipments.Add(book4);
            context.CheckOuts.Add(checkOut4);
            checkOut4.Equipment.Available = false;
            checkOut4.Equipment.InWarehouse = false;
            checkOut4.Equipment.Location = book4.Commission.Location;
            checkOut4.ArriveTime = checkOut4.CreationTime.AddHours(2);



            var checkIn1 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment1,
                EquipmentId = equipment1.Id,
                Id = Guid.NewGuid(),
                CreationTime = checkOut1.EndTime
            };
            var checkIn2 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment2,
                EquipmentId = equipment2.Id,
                Id = Guid.NewGuid(),
                CreationTime = checkOut2.EndTime
            };

            var checkIn3 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment3,
                EquipmentId = equipment3.Id,
                Id = Guid.NewGuid(),
                CreationTime = checkOut3.EndTime
            };

            context.CheckIns.Add(checkIn1);
            checkIn1.Equipment.Available = true;
            checkIn1.ArriveTime = checkIn1.CreationTime.AddHours(2);
            checkIn1.Equipment.InWarehouse = true;
            checkIn1.Equipment.Location = "warehouse 1";

            context.CheckIns.Add(checkIn2);
            checkIn2.Equipment.Available = true;
            checkIn2.ArriveTime = checkIn2.CreationTime.AddHours(5);
            checkIn2.Equipment.InWarehouse = true;
            checkIn2.Equipment.Location = "warehouse 2";

            context.CheckIns.Add(checkIn3);
            checkIn3.Equipment.Available = true;
            checkIn3.Equipment.Location = "on the way to warehouse 3";
            //checkIn3.ArriveTime = checkIn3.CreationTime.AddHours(1);
            //checkIn3.Equipment.InWarehouse = true;
            //checkIn3.Equipment.Location = "warehouse 3";


            var invite1 = new Invite()
            {
                UserId = identityContext.Users.First().Id,
                Company = company1,
                CompanyId = company1.Id,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Status = Status.Sent
            };
            var invite2 = new Invite()
            {
                UserId = identityContext.Users.First().Id,
                Company = company1,
                CompanyId = company1.Id,
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