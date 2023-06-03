using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Medicine
    {
        public Guid MedicineId { get; set; }

        [Required(ErrorMessage = "Medicine Name is required")]
        [StringLength(60, ErrorMessage = "Medicine Name can't be longer than 60 characters")]
        public string? MedicineName { get; set; }

        [Required(ErrorMessage = "Brand Name is required")]
        [StringLength(60, ErrorMessage = "Brand Name can't be longer than 60 characters")]
        public string? BrandName { get; set; }

        [Required(ErrorMessage = "Expire Date is required")]
        public DateTime ExpireDate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public Double Price { get; set; }

        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        public Supplier? Supplier { get; set; }
    }
}

