using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class MedicineParameters
    {
        [StringLength(60, ErrorMessage = "Medicine Name can't be longer than 60 characters")]
        public string? MedicineName { get; set; }

        [StringLength(60, ErrorMessage = "Brand Name can't be longer than 60 characters")]
        public string? BrandName { get; set; }

        public Double? PriceStart { get; set; }

        public Double? PriceEnd { get; set; }

        public DateTime? ExpireDateStart { get; set; }

        public DateTime? ExpireDateEnd { get; set; }

        public Guid? SupplierId { get; set; }
    }
}

