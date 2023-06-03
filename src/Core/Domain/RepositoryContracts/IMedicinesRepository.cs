using System;
using Core.Domain.Entities;
using Core.DTO;

namespace Core.Domain.RepositoryContracts
{
    public interface IMedicinesRepository : IRepositoryBase<Medicine>
    {
        Task<IEnumerable<Medicine>> GetAllMedicines();
        Task<Medicine?> GetMedicineById(Guid medicineId);
        void CreateMedicine(Medicine medicine);
        void UpdateMedicine(Medicine medicine);
        void DeleteMedicine(Medicine medicine);
        Task<IEnumerable<Medicine>> GetMedicinesBySupplier(Guid medicineId);
        Task<bool> IsDuplicateName(string medicineName);
        Task<bool> IsDuplicateName(string medicineName, Guid medicineId);
        Task<IEnumerable<Medicine>> GetMedicines(MedicineParameters parameters);
    }
}

