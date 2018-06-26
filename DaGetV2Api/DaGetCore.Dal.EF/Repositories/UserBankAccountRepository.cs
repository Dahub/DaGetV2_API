using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DaGetCore.Dal.EF
{
    internal class UserBankAccountRepository : RepositoryBase<UserBankAccount>, IUserBankAccountRepository
    {
        public IEnumerable<UserBankAccount> GetByUserPublicId(Guid userId)
        {
            return ((DaGetContext)Context).UsersBankAccounts.Where(uba => uba.UserId.Equals(userId));
        }        

        public UserBankAccount GetByUserPublicIdAndBankAccountId(Guid userId, int bankAccountId)
        {
            return ((DaGetContext)Context).UsersBankAccounts.Where(uba => uba.UserId.Equals(userId) && uba.BankAccountId.Equals(bankAccountId)).FirstOrDefault();
        }
    }
}
