using System;
using System.Collections.Generic;
using System.Text;
using DaGetCore.Service.Dto;

namespace DaGetCore.Service
{
    public class OperationService : ServiceBase, IOperationService
    {
        public OperationDto Create(Guid? userId, OperationDto toCreate)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid? userId, OperationDto toDelete)
        {
            throw new NotImplementedException();
        }

        public OperationDto GetById(Guid? userId, int id)
        {
            throw new NotImplementedException();
        }

        public OperationDto Update(Guid? userId, OperationDto toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
