using System;
using Moq;
using System.Security.Principal;
using Core.Domain.RepositoryContracts;
using Core.Domain.Entities;

namespace ControllerTests.Mocks
{
    internal class MockISupplierRepository
    {
        public static Mock<ISuppliersRepository> GetMock()
        {
            var mock = new Mock<ISuppliersRepository>();

            var suppliers = new List<Supplier>()
            {
                new Supplier()
                {
                    SupplierId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    SupplierName = "Hospira, Inc.",
                    Address = "125 Corscot Way",
                    PhoneNumber = "9704080208"
                }
            };

            mock.Setup(m => m.GetAllSuppliers()).ReturnsAsync(() => suppliers);

            mock.Setup(m => m.GetSupplierById(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => suppliers.FirstOrDefault(s => s.SupplierId == id));

            mock.Setup(m => m.CreateSupplier(It.IsAny<Supplier>()))
                .Callback(() => { return; });

            mock.Setup(m => m.UpdateSupplier(It.IsAny<Supplier>()))
                .Callback(() => { return; });

            mock.Setup(m => m.DeleteSupplier(It.IsAny<Supplier>()))
                .Callback(() => { return; });

            mock.Setup(m => m.IsDuplicateName(It.IsAny<string>()))
                .ReturnsAsync((string name) => suppliers.Where(s => s.SupplierName.Equals(name)).Count() > 0);

            return mock;
        }
    }
}

