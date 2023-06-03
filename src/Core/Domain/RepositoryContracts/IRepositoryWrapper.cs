using System;
namespace Core.Domain.RepositoryContracts
{
    public interface IRepositoryWrapper
    {
        ISuppliersRepository Suppliers { get; }
        IMedicinesRepository Medicines { get; }

        Task SaveAsync();
    }
}

