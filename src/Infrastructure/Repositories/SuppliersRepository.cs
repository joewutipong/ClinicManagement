using System;
using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SuppliersRepository : RepositoryBase<Supplier>, ISuppliersRepository
    {
        public SuppliersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            return await FindAll().OrderBy(temp => temp.SupplierName).ToListAsync();
        }

        public async Task<Supplier?> GetSupplierById(Guid supplierId)
        {
            return await FindByCondition(temp => temp.SupplierId.Equals(supplierId)).FirstOrDefaultAsync();
        }

        public void CreateSupplier(Supplier supplier)
        {
            Create(supplier);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            Update(supplier);
        }

        public void DeleteSupplier(Supplier supplier)
        {
            Delete(supplier);
        }

        public async Task<bool> IsDuplicateName(string supplierName)
        {
            var supplier = await FindByCondition(temp => temp.SupplierName.Equals(supplierName)).ToListAsync();

            return supplier.Count() > 0;
        }

        public async Task<bool> IsDuplicateName(string supplierName, Guid supplierId)
        {
            var supplier = await FindByCondition(temp => temp.SupplierName.Equals(supplierName) && temp.SupplierId != supplierId).ToListAsync();

            return supplier.Count() > 0;
        }
    }
}

