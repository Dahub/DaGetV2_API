using DaGetCore.Service.Dto;
using System;

namespace DaGetCore.Service
{
    public interface IBankAccountService
    {
        BankAccountDto GetById(Guid? userId, int id);
        BankAccountDto Create(Guid? userId, string userName, BankAccountDto toCreate);
        BankAccountDto Update(Guid? userId, BankAccountDto toUpdate);
    }
}
