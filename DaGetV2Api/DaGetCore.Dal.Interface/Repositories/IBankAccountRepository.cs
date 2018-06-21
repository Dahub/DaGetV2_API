using DaGetCore.Domain;
using System;
using System.Collections.Generic;

namespace DaGetCore.Dal.Interface
{
    public interface IBankAccountRepository : IRepository
    {
        void Add(BankAccount toAdd);
        void Update(BankAccount toUpdate);
        void Delete(BankAccount toDelete);
        IEnumerable<BankAccount> GetAllByIdUser(Guid userId);
        BankAccount GetAllByIdUserAndId(Guid userId, int id);
    }
}
