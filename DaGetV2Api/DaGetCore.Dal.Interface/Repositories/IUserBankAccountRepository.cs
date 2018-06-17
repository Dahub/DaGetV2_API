using DaGetCore.Domain;
using System;

namespace DaGetCore.Dal.Interface
{
    public interface IUserBankAccountRepository : IRepository
    {
        UserBankAccount GetByUserPublicId(Guid userId);
        void Add(UserBankAccount toAdd);
    }
}
