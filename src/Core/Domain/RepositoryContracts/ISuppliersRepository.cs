using System;
using Core.Domain.Entities;

namespace Core.Domain.RepositoryContracts
{
    public interface ISuppliersRepository : IRepositoryBase<Supplier>
    {
        Task<IEnumerable<Supplier>> GetAllSuppliers();
        Task<Supplier?> GetSupplierById(Guid supplierId);
        void CreateSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(Supplier supplier);
        Task<bool> IsDuplicateName(string supplierName);
        Task<bool> IsDuplicateName(string supplierName, Guid supplierId);
    }
}

