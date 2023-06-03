using System;
using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Core.DTO;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MedicinesRepository : RepositoryBase<Medicine>, IMedicinesRepository
    {
        public MedicinesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Medicine>> GetAllMedicines()
        {
            return await FindAll().OrderBy(temp => temp.MedicineName).ToListAsync();
        }

        public async Task<Medicine?> GetMedicineById(Guid medicineId)
        {
            return await FindByCondition(temp => temp.MedicineId.Equals(medicineId))
                .Include(temp => temp.Supplier)
                .FirstOrDefaultAsync();
        }

        public void CreateMedicine(Medicine medicine)
        {
            Create(medicine);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            Update(medicine);
        }

        public void DeleteMedicine(Medicine medicine)
        {
            Delete(medicine);
        }

        public async Task<IEnumerable<Medicine>> GetMedicinesBySupplier(Guid supplierId)
        {
            return await FindByCondition(temp => temp.SupplierId.Equals(supplierId)).ToListAsync();
        }

        public async Task<bool> IsDuplicateName(string medicineName)
        {
            var medicine = await FindByCondition(temp => temp.MedicineName.Equals(medicineName)).ToListAsync();

            return medicine.Count() > 0;
        }

        public async Task<bool> IsDuplicateName(string medicineName, Guid medicineId)
        {
            var medicine = await FindByCondition(temp => temp.MedicineName.Equals(medicineName) && temp.MedicineId != medicineId).ToListAsync();

            return medicine.Count() > 0;
        }

        public async Task<IEnumerable<Medicine>> GetMedicines(MedicineParameters parameters)
        {
            var medicines = FindByCondition(temp => temp.MedicineId != null);

            if (parameters.MedicineName != null)
            {
                medicines = medicines.Where(temp => temp.MedicineName.Contains(parameters.MedicineName));
            }

            if (parameters.BrandName != null)
            {
                medicines = medicines.Where(temp => temp.BrandName.Contains(parameters.BrandName));
            }

            if (parameters.PriceStart != null)
            {
                medicines = medicines.Where(temp => temp.Price >= parameters.PriceStart);
            }

            if (parameters.PriceEnd != null)
            {
                medicines = medicines.Where(temp => temp.Price <= parameters.PriceEnd);
            }

            if (parameters.ExpireDateStart != null)
            {
                var expireDate = parameters.ExpireDateStart.Value.AddDays(1).AddTicks(-1);
                medicines = medicines.Where(temp => temp.ExpireDate >= expireDate);
            }

            if (parameters.ExpireDateEnd != null)
            {
                var expireDate = parameters.ExpireDateEnd.Value.AddDays(1).AddTicks(-1);
                medicines = medicines.Where(temp => temp.ExpireDate <= expireDate);
            }

            if (parameters.SupplierId != null)
            {
                medicines = medicines.Where(temp => temp.SupplierId.Equals(parameters.SupplierId));
            }

            return await medicines.OrderBy(temp => temp.MedicineName).ToListAsync();
        }
    }
}

