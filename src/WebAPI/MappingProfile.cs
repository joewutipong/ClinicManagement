using System;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTO;

namespace WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<SupplierForCreationDTO, Supplier>();
            CreateMap<SupplierForUpdateDTO, Supplier>();

            CreateMap<Medicine, MedicineDTO>();
            CreateMap<MedicineForCreationDTO, Medicine>();
            CreateMap<MedicineForUpdateDTO, Medicine>();
        }
    }
}

