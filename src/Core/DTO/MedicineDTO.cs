using System;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DTO
{
    public class MedicineDTO
    {
        public Guid MedicineId { get; set; }

        public string? MedicineName { get; set; }

        public string? BrandName { get; set; }

        public DateTime ExpireDate { get; set; }

        public Double Price { get; set; }

        public Guid SupplierId { get; set; }

        public Supplier? Supplier { get; set; }
    }
}

