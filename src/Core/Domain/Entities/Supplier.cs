using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Supplier
    {
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier Name is required")]
        [StringLength(60, ErrorMessage = "Supplier Name can't be longer than 60 characters")]
        public string? SupplierName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 60 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(10, ErrorMessage = "Phone Number can't be longer than 60 characters")]
        public string? PhoneNumber { get; set; }
    }
}

