using DaGetCore.Domain;
using System;
using System.Collections.Generic;

namespace DaGetCore.Dal.Interface
{
    public interface IUserBankAccountRepository : IRepository
    {
        UserBankAccount GetByUserPublicIdAndBankAccountId(Guid userId, int bankAccountId);
        IEnumerable<UserBankAccount> GetByUserPublicId(Guid userId);
        void Add(UserBankAccount toAdd);
    }
}
