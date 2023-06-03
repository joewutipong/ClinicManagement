using System;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Supplier>? Suppliers { get; set; }
        public DbSet<Medicine>? Medicines { get; set; }
    }
}

