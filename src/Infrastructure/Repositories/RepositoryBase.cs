using System;
using System.Linq.Expressions;
using Core.Domain.RepositoryContracts;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext DbContext { get; set; }

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<T> FindAll() => DbContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => DbContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => DbContext.Set<T>().Add(entity);

        public void Update(T entity) => DbContext.Set<T>().Update(entity);

        public void Delete(T entity) => DbContext.Set<T>().Remove(entity);
    }
}

