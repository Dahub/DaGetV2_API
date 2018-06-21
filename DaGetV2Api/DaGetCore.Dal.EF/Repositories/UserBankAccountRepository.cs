using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DaGetCore.Dal.EF
{
    public class UserBankAccountRepository : IUserBankAccountRepository
    {
        public IContext Context { get; set; }

        public IEnumerable<UserBankAccount> GetByUserPublicId(Guid userId)
        {
            return ((DaGetContext)Context).UsersBankAccounts.Where(uba => uba.UserId.Equals(userId));
        }

        public void Add(UserBankAccount toAdd)
        {
            ((DbContext)Context).Set<UserBankAccount>().Add(toAdd);
        }

        public UserBankAccount GetByUserPublicIdAndBankAccountId(Guid userId, int bankAccountId)
        {
            return ((DaGetContext)Context).UsersBankAccounts.Where(uba => uba.UserId.Equals(userId) && uba.BankAccountId.Equals(bankAccountId)).FirstOrDefault();
        }
    }
}
