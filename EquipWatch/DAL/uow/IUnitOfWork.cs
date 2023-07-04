﻿using DAL.Repositories.Client;
using DAL.Repositories.Company;
using DAL.Repositories.Equipment;

namespace webapi.uow
{
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }
        ICompanyRepository Companies { get; }
        IEquipmentRepository Equipments { get; }
        // Add other repositories as needed

        void SaveChanges();
        Task SaveChangesAsync();
    }
}