using System;
using Core.Domain.RepositoryContracts;
using Moq;

namespace ControllerTests.Mocks
{
    internal class MockIRepositoryWrapper
    {
        public static Mock<IRepositoryWrapper> GetMock()
        {
            var mock = new Mock<IRepositoryWrapper>();
            var supplierRepoMock = MockISupplierRepository.GetMock();

            mock.Setup(m => m.Suppliers).Returns(() => supplierRepoMock.Object);
            mock.Setup(m => m.SaveAsync()).Callback(() => { return; });

            return mock;
        }
    }
}

