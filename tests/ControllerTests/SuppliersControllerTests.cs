using System;
using AutoMapper;
using ControllerTests.Mocks;
using Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI;
using WebAPI.Controllers.v1;

namespace ControllerTests
{
    public class SuppliersControllerTests
    {
        public IMapper GetMapper()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }

        [Fact]
        public async void WhenGettingAllSuppliers_ThenAllSuppliersReturn()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var supplierController = new SuppliersController(repositoryWrapperMock.Object, mapper);

            var result = await supplierController.Index() as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<SupplierDTO>>(result.Value);
            Assert.NotEmpty(result.Value as IEnumerable<SupplierDTO>);
        }

        [Fact]
        public async void GivenAnIdOfAnExistingSupplier_WhenGettingSupplierById_ThenSupplierReturns()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var supplierController = new SuppliersController(repositoryWrapperMock.Object, mapper);

            var id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e");
            var result = await supplierController.GetSupplierById(id) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<SupplierDTO>(result.Value);
            Assert.NotNull(result.Value as SupplierDTO);
        }

        [Fact]
        public async void GivenAnIdOfANonExistingSupplier_WhenGettingSupplierById_ThenNotFoundReturns()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var supplierController = new SuppliersController(repositoryWrapperMock.Object, mapper);

            var id = Guid.NewGuid();
            var result = await supplierController.GetSupplierById(id) as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async void GivenValidRequest_WhenCreatingSupplier_ThenSupplierReturns()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var supplierController = new SuppliersController(repositoryWrapperMock.Object, mapper);
            var supplier = new SupplierForCreationDTO()
            {
                SupplierName = "SJ Creations, Inc.",
                Address = "3 Ramsey Plaza",
                PhoneNumber = "9325225409"
            };

            var result = await supplierController.CreateSupplier(supplier) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<SupplierDTO>(result.Value);
            Assert.NotNull(result.Value as SupplierDTO);
        }

        [Fact]
        public async void GivenDuplicateName_WhenCreatingSupplier_ThenBadRequestReturns()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var supplierController = new SuppliersController(repositoryWrapperMock.Object, mapper);
            var supplier = new SupplierForCreationDTO()
            {
                SupplierName = "Hospira, Inc.",
                Address = "3 Ramsey Plaza",
                PhoneNumber = "9325225409"
            };

            var result = await supplierController.CreateSupplier(supplier) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal("Supplier Name is duplicate", result.Value);
        }
    }
}

