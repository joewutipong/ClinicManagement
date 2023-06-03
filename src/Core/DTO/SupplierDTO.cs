using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class SupplierDTO
    {
        public Guid SupplierId { get; set; }

        public string? SupplierName { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
    }
}

