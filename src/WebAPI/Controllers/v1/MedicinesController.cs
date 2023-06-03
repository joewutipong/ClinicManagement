using System;
using System.Net;
using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Core.DTO;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.v1
{
    public class MedicinesController : CustomControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public MedicinesController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] MedicineParameters parameters)
        {
            var medicines = await _repository.Medicines.GetMedicines(parameters);

            var medicinesResult = _mapper.Map<IEnumerable<MedicineDTO>>(medicines);

            return Ok(medicinesResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicineById(Guid id)
        {
            var medicine = await _repository.Medicines.GetMedicineById(id);

            if (medicine is null)
            {
                return NotFound();
            }

            var medicineResult = _mapper.Map<MedicineDTO>(medicine);

            return Ok(medicineResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicine([FromBody] MedicineForCreationDTO medicine)
        {
            if (medicine is null)
            {
                return BadRequest("Medicine object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid medicine object");
            }

            var isDuplicateName = await _repository.Medicines.IsDuplicateName(medicine.MedicineName);

            if (isDuplicateName)
            {
                return BadRequest("Medicine name is duplicate");
            }

            var supplier = await _repository.Suppliers.GetSupplierById(medicine.SupplierId);

            if (supplier is null)
            {
                return NotFound("Supplier not found");
            }

            var medicineEntity = _mapper.Map<Medicine>(medicine);

            _repository.Medicines.CreateMedicine(medicineEntity);
            await _repository.SaveAsync();

            var createdMedicine = _mapper.Map<MedicineDTO>(medicineEntity);

            return Ok(createdMedicine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicine(Guid id, [FromBody] MedicineForUpdateDTO medicine)
        {
            if (medicine is null)
            {
                return BadRequest("Medicine object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid medicine object");
            }

            var medicineEntity = await _repository.Medicines.GetMedicineById(id);

            if (medicineEntity is null)
            {
                return NotFound();
            }

            var isDuplicateName = await _repository.Medicines.IsDuplicateName(medicine.MedicineName, medicineEntity.MedicineId);

            if (isDuplicateName)
            {
                return BadRequest("Medicine name is duplicate");
            }

            var supplier = await _repository.Suppliers.GetSupplierById(medicine.SupplierId);

            if (supplier is null)
            {
                return NotFound("Supplier not found");
            }

            _mapper.Map(medicine, medicineEntity);

            _repository.Medicines.UpdateMedicine(medicineEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(Guid id)
        {
            var medicine = await _repository.Medicines.GetMedicineById(id);

            if (medicine is null)
            {
                return NotFound();
            }

            _repository.Medicines.DeleteMedicine(medicine);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}

