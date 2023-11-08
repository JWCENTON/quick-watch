using Domain.BookedEquipment.Models;
using Domain.CheckIn.Models;
using Domain.CheckOut.Models;
using Domain.Client.Models;
using Domain.Commission.Models.Commission;
using Domain.Company.Models;
using Domain.Equipment.Models;
using Domain.EquipmentInUse.Models;
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

            context.Company.Add(company1);

            var equipment1 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "4725375345",
                Category = "Fog machine",
                Location = "Main warehouse",
                Condition = 4,
                Available = true,
                InWarehouse = true
            };
            var equipment2 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "7638263963",
                Category = "Fog machine",
                Location = "Main warehouse",
                Condition = 3,
                Available = true,
                InWarehouse = true
            };
            var equipment3 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "6253791364",
                Category = "Fog machine",
                Location = "Main warehouse",
                Condition = 5,
                Available = true,
                InWarehouse = true
            };
            var equipment4 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "7462684743",
                Category = "Fog machine",
                Location = "Main warehouse",
                Condition = 1,
                Available = true,
                InWarehouse = true
            };

            var equipment5 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "7462653743",
                Category = "Fog machine",
                Location = "Main warehouse",
                Condition = 2,
                Available = true,
                InWarehouse = true
            };

            var equipment6 = new Equipment
            {
                Id = Guid.NewGuid(),
                SerialNumber = "7463453243",
                Category = "Fog machine",
                Location = "Main warehouse",
                Condition = 5,
                Available = true,
                InWarehouse = true
            };

            equipment1.Company = company1;
            equipment2.Company = company1;
            equipment3.Company = company1;
            equipment4.Company = company1;
            equipment5.Company = company1;
            equipment6.Company = company1;
            context.Equipment.Add(equipment1);
            context.Equipment.Add(equipment2);
            context.Equipment.Add(equipment3);
            context.Equipment.Add(equipment4);
            context.Equipment.Add(equipment5);
            context.Equipment.Add(equipment6);

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
                CreationTime = DateTime.Now - TimeSpan.FromDays(12),
                StartTime = DateTime.Now - TimeSpan.FromDays(10),
                EndTime = DateTime.Now.AddDays(50)
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
                CreationTime = DateTime.Now - TimeSpan.FromDays(10),
                StartTime = DateTime.Now - TimeSpan.FromDays(8)
            };
            context.Commissions.Add(commission1);
            context.Commissions.Add(commission2);

            var work1 = new WorksOn()
            {
                Id = Guid.NewGuid(),
                Commission = commission1,
                CommissionId = commission1.Id,
                UserId = identityContext.Users.First().Id,
                CreationTime = DateTime.Now,
                EndTime = null
            };
            var work2 = new WorksOn()
            {
                Id = Guid.NewGuid(),
                Commission = commission2,
                CommissionId = commission2.Id,
                UserId = identityContext.Users.First().Id,
                CreationTime = DateTime.Now.AddMinutes(1),
                EndTime = null  
            };
            var work3 = new WorksOn()
            {
                Id = Guid.NewGuid(),
                Commission = commission2,
                CommissionId = commission2.Id,
                UserId = identityContext.Users.First().Id,
                CreationTime = DateTime.Now,
                EndTime = DateTime.Now.AddSeconds(50)
            };
            context.WorksOn.Add(work1);
            context.WorksOn.Add(work2);
            context.WorksOn.Add(work3);

            ////////////

            var equipmentInUse1 = new EquipmentInUse()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(10),
                Equipment = equipment1,
                EquipmentId = equipment1.Id,
                UserId = identityContext.Users.First().Id
            };

            var book1 = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                Commission = commission1,
                CommissionId = commission1.Id,
                EquipmentInUse = equipmentInUse1,
                EquipmentInUseId = equipmentInUse1.Id,
                CreationTime = DateTime.Now,
                EndTime = null
            };

            context.BookedEquipments.Add(book1);
            
            var checkOut1 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse1.Equipment,
                EquipmentId = equipmentInUse1.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                WarehouseDelivery = false
            };

            var checkIn1 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse1.Equipment,
                EquipmentId = equipmentInUse1.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddHours(2),
                WarehouseDelivery = false
            };

            context.CheckOuts.Add(checkOut1);
            checkOut1.Equipment.Available = false;
            checkOut1.Equipment.InWarehouse = false;
            checkOut1.Equipment.Location = "On the way to " + book1.Commission.Location;

            context.CheckIns.Add(checkIn1);
            checkIn1.Equipment.Location = book1.Commission.Location;

            var warehouseCheckOut1 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse1.Equipment,
                EquipmentId = equipmentInUse1.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddDays(10).AddHours(2),
                WarehouseDelivery = true
            };

            var warehouseCheckIn1 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse1.Equipment,
                EquipmentId = equipmentInUse1.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddDays(10).AddHours(4),
                WarehouseDelivery = true
            };

            context.CheckOuts.Add(warehouseCheckOut1);
            warehouseCheckOut1.Equipment.Available = true;
            warehouseCheckOut1.Equipment.Location = "On the way to main warehouse";
            book1.EndTime = DateTime.Now.AddDays(10).AddHours(4);

            context.CheckIns.Add(warehouseCheckIn1);
            warehouseCheckIn1.Equipment.InWarehouse = true;
            warehouseCheckIn1.Equipment.Location = "main warehouse";

            ////////////

            var equipmentInUse2 = new EquipmentInUse()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(15),
                Equipment = equipment2,
                EquipmentId = equipment2.Id,
                UserId = identityContext.Users.First().Id
            };

            var book2 = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Commission = commission1,
                CommissionId = commission1.Id,
                EquipmentInUse = equipmentInUse2,
                EquipmentInUseId = equipmentInUse2.Id,
                EndTime = null
            };

            context.BookedEquipments.Add(book2);

            var checkOut2 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse2.Equipment,
                EquipmentId = equipmentInUse2.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                WarehouseDelivery = false
            };

            var checkIn2 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse2.Equipment,
                EquipmentId = equipmentInUse2.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddHours(10),
                WarehouseDelivery = false
            };

            context.CheckOuts.Add(checkOut2);
            checkOut2.Equipment.Available = false;
            checkOut2.Equipment.InWarehouse = false;
            checkOut2.Equipment.Location = "On the way to " + book2.Commission.Location;

            context.CheckIns.Add(checkIn2);
            checkIn2.Equipment.Location = book2.Commission.Location;

            var warehouseCheckOut2 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse2.Equipment,
                EquipmentId = equipmentInUse2.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddDays(10).AddHours(2),
                WarehouseDelivery = true
            };

            var warehouseCheckIn2 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse2.Equipment,
                EquipmentId = equipmentInUse2.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddDays(10).AddHours(14),
                WarehouseDelivery = true
            };

            context.CheckOuts.Add(warehouseCheckOut2);
            warehouseCheckOut2.Equipment.Available = true;
            warehouseCheckOut2.Equipment.Location = "On the way to main warehouse";
            book2.EndTime = DateTime.Now.AddDays(10).AddHours(2);

            context.CheckIns.Add(warehouseCheckIn2);
            warehouseCheckIn2.Equipment.InWarehouse = true;
            warehouseCheckIn2.Equipment.Location = "main warehouse";

            ////////////

            var equipmentInUse3 = new EquipmentInUse()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Equipment = equipment3,
                EquipmentId = equipment3.Id,
                UserId = identityContext.Users.First().Id
            };

            var book3 = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Commission = commission1,
                CommissionId = commission1.Id,
                EquipmentInUse = equipmentInUse3,
                EquipmentInUseId = equipmentInUse3.Id,
                EndTime = null
            };

            context.BookedEquipments.Add(book3);

            var checkOut3 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse3.Equipment,
                EquipmentId = equipmentInUse3.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                WarehouseDelivery = false
            };

            var checkIn3 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse3.Equipment,
                EquipmentId = equipmentInUse3.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddHours(4),
                WarehouseDelivery = false
            };

            context.CheckOuts.Add(checkOut3);
            checkOut3.Equipment.Available = false;
            checkOut3.Equipment.InWarehouse = false;
            checkOut3.Equipment.Location = "On the way to " + book3.Commission.Location;

            context.CheckIns.Add(checkIn3);
            checkIn3.Equipment.Location = book3.Commission.Location;

            ////////////
            
            var equipmentInUse4 = new EquipmentInUse()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(20),
                Equipment = equipment4,
                EquipmentId = equipment4.Id,
                UserId = identityContext.Users.First().Id
            };

            var book4 = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Commission = commission2,
                CommissionId = commission2.Id,
                EquipmentInUse = equipmentInUse4,
                EquipmentInUseId = equipmentInUse4.Id,
                EndTime = null
            };

            context.BookedEquipments.Add(book4);

            var checkOut4 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse4.Equipment,
                EquipmentId = equipmentInUse4.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                WarehouseDelivery = false
            };


            context.CheckOuts.Add(checkOut4);
            checkOut4.Equipment.Available = false;
            checkOut4.Equipment.InWarehouse = false;
            checkOut4.Equipment.Location = "On the way to " + book4.Commission.Location;

            ////////////

            var equipmentInUse5 = new EquipmentInUse()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Equipment = equipment5,
                EquipmentId = equipment5.Id,
                UserId = identityContext.Users.First().Id
            };

            var book5 = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Commission = commission2,
                CommissionId = commission2.Id,
                EquipmentInUse = equipmentInUse5,
                EquipmentInUseId = equipmentInUse5.Id,
                EndTime = null
            };

            context.BookedEquipments.Add(book5);

            var checkOut5 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse5.Equipment,
                EquipmentId = equipmentInUse5.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                WarehouseDelivery = false
            };

            var checkIn5 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse5.Equipment,
                EquipmentId = equipmentInUse5.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddHours(4),
                WarehouseDelivery = false
            };

            context.CheckOuts.Add(checkOut5);
            checkOut5.Equipment.Available = false;
            checkOut5.Equipment.InWarehouse = false;
            checkOut5.Equipment.Location = "On the way to " + book5.Commission.Location;

            context.CheckIns.Add(checkIn5);
            checkIn5.Equipment.Location = book5.Commission.Location;

            var warehouseCheckOut3 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse5.Equipment,
                EquipmentId = equipmentInUse5.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddDays(15).AddHours(2),
                WarehouseDelivery = true
            };

            context.CheckOuts.Add(warehouseCheckOut3);
            warehouseCheckOut3.Equipment.Available = true;
            warehouseCheckOut3.Equipment.Location = "On the way to main warehouse";
            book5.EndTime = DateTime.Now.AddDays(15).AddHours(2);

            /////////

            var equipmentInUse6 = new EquipmentInUse()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Equipment = equipment6,
                EquipmentId = equipment6.Id,
                UserId = identityContext.Users.First().Id
            };

            var book6 = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Commission = commission2,
                CommissionId = commission2.Id,
                EquipmentInUse = equipmentInUse6,
                EquipmentInUseId = equipmentInUse6.Id,
                EndTime = null
            };

            context.BookedEquipments.Add(book6);

            var checkOut6 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse6.Equipment,
                EquipmentId = equipmentInUse6.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                WarehouseDelivery = false
            };

            var checkIn6 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse6.Equipment,
                EquipmentId = equipmentInUse6.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddHours(4),
                WarehouseDelivery = false
            };

            context.CheckOuts.Add(checkOut6);
            checkOut6.Equipment.Available = false;
            checkOut6.Equipment.InWarehouse = false;
            checkOut6.Equipment.Location = "On the way to " + book6.Commission.Location;

            context.CheckIns.Add(checkIn6);
            checkIn6.Equipment.Location = book6.Commission.Location;


            /////////
            
            var reservation1 = new Reservation
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipment1,
                EquipmentId = equipment1.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                StartTime = DateTime.Now.AddDays(11),
                EndTime = DateTime.Now.AddDays(25)
            };

            var book7 = new BookedEquipment()
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                Commission = commission1,
                CommissionId = commission1.Id,
                Reservation = reservation1,
                ReservationId = reservation1.Id,
                EndTime = null
            };

            var equipmentInUse7 = new EquipmentInUse()
            {
                Id = Guid.NewGuid(),
                CreationTime = reservation1.StartTime,
                EndTime = reservation1.EndTime,
                Equipment = reservation1.Equipment,
                EquipmentId = reservation1.Equipment.Id,
                UserId = identityContext.Users.First().Id
            };

            book7.EquipmentInUse = equipmentInUse7;
            book7.EquipmentInUseId = equipmentInUse7.Id;

            var checkOut7 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse7.Equipment,
                EquipmentId = equipmentInUse7.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = equipmentInUse7.CreationTime,
                WarehouseDelivery = false
            };

            var checkIn7 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse7.Equipment,
                EquipmentId = equipmentInUse7.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = equipmentInUse7.CreationTime.AddHours(10),
                WarehouseDelivery = false
            };

            context.CheckOuts.Add(checkOut7);
            checkOut7.Equipment.Available = false;
            checkOut7.Equipment.InWarehouse = false;
            checkOut7.Equipment.Location = "On the way to " + book7.Commission.Location;

            context.CheckIns.Add(checkIn7);
            checkIn7.Equipment.Location = book7.Commission.Location;

            var warehouseCheckOut7 = new CheckOut()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse7.Equipment,
                EquipmentId = equipmentInUse7.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddDays(25).AddHours(2),
                WarehouseDelivery = true
            };

            var warehouseCheckIn7 = new CheckIn()
            {
                UserId = identityContext.Users.First().Id,
                Equipment = equipmentInUse7.Equipment,
                EquipmentId = equipmentInUse7.Equipment.Id,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now.AddDays(25).AddHours(10),
                WarehouseDelivery = true
            };

            context.CheckOuts.Add(warehouseCheckOut7);
            warehouseCheckOut7.Equipment.Available = true;
            warehouseCheckOut7.Equipment.Location = "On the way to main warehouse";
            book1.EndTime = DateTime.Now.AddDays(25).AddHours(2);

            context.CheckIns.Add(warehouseCheckIn7);
            warehouseCheckIn7.Equipment.InWarehouse = true;
            warehouseCheckIn7.Equipment.Location = "main warehouse";

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