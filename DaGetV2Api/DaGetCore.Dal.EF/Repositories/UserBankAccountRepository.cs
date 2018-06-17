using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DaGetCore.Dal.EF
{
    public class UserBankAccountRepository : IUserBankAccountRepository
    {
        public IContext Context { get; set; }

        public UserBankAccount GetByUserPublicId(Guid userId)
        {
            return ((DaGetContext)Context).UsersBankAccounts.Where(uba => uba.UserId.Equals(userId)).FirstOrDefault();
        }

        public void Add(UserBankAccount toAdd)
        {
            ((DbContext)Context).Set<UserBankAccount>().Add(toAdd);
        }
    }
}
