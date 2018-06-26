using DaGetCore.Service.Dto;
using System;

namespace DaGetCore.Service
{
    public interface IOperationService
    {
        OperationDto GetById(Guid? userId, int id);
        OperationDto Create(Guid? userId, OperationDto toCreate);
        OperationDto Update(Guid? userId, OperationDto toUpdate);
        void Delete(Guid? userId, OperationDto toDelete);
    }
}
