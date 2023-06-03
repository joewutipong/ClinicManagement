using System;
using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    public class SuppliersController : CustomControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public SuppliersController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var suppliers = await _repository.Suppliers.GetAllSuppliers();

            var suppliersResult = _mapper.Map<IEnumerable<SupplierDTO>>(suppliers);

            return Ok(suppliersResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(Guid id)
        {
            var supplier = await _repository.Suppliers.GetSupplierById(id);

            if (supplier is null)
            {
                return NotFound();
            }

            var supplierResult = _mapper.Map<SupplierDTO>(supplier);

            return Ok(supplierResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierForCreationDTO supplier)
        {
            if (supplier is null)
            {
                return BadRequest("Supplier object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid supplier object");
            }

            var isDuplicateName = await _repository.Suppliers.IsDuplicateName(supplier.SupplierName);

            if (isDuplicateName)
            {
                return BadRequest("Supplier Name is duplicate");
            }

            var supplierEntity = _mapper.Map<Supplier>(supplier);

            _repository.Suppliers.CreateSupplier(supplierEntity);
            await _repository.SaveAsync();

            var createdSupplier = _mapper.Map<SupplierDTO>(supplierEntity);

            return Ok(createdSupplier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] SupplierForUpdateDTO supplier)
        {
            if (supplier is null)
            {
                return BadRequest("Supplier object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid supplier object");
            }

            var supplierEntity = await _repository.Suppliers.GetSupplierById(id);

            if (supplierEntity is null)
            {
                return NotFound();
            }

            var isDuplicateName = await _repository.Suppliers.IsDuplicateName(supplier.SupplierName, supplierEntity.SupplierId);

            if (isDuplicateName)
            {
                return BadRequest("Supplier Name is duplicate");
            }

            _mapper.Map(supplier, supplierEntity);

            _repository.Suppliers.UpdateSupplier(supplierEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            var supplier = await _repository.Suppliers.GetSupplierById(id);

            if (supplier is null)
            {
                return NotFound();
            }

            var medicines = await _repository.Medicines.GetMedicinesBySupplier(id);

            if (medicines.Count() > 0)
            {
                return BadRequest("Can't delete supplier. It has related medicine. Delete those medicines first");
            }

            _repository.Suppliers.DeleteSupplier(supplier);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}

