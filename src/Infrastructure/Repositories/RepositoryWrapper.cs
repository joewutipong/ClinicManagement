using System;
using Core.Domain.RepositoryContracts;
using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _dbContext;

        private ISuppliersRepository _supplier;
        private IMedicinesRepository _medicine;

        public RepositoryWrapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ISuppliersRepository Suppliers
        {
            get
            {
                if (_supplier == null)
                {
                    _supplier = new SuppliersRepository(_dbContext);
                }

                return _supplier;
            }
        }

        public IMedicinesRepository Medicines
        {
            get
            {
                if (_medicine == null)
                {
                    _medicine = new MedicinesRepository(_dbContext);
                }

                return _medicine;
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

