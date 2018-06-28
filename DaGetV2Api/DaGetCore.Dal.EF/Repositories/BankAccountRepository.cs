using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DaGetCore.Dal.EF
{
    internal class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {    
        public IEnumerable<BankAccount> GetAllByIdUser(Guid userId)
        {
            return ((DaGetContext)Context).UsersBankAccounts.Where(uba => uba.UserId.Equals(userId)).Select(uba => uba.BankAccount);
        }

        public BankAccount GetByIdUserAndId(Guid userId, int id)
        {
            return ((DaGetContext)Context).UsersBankAccounts.Where(uba => uba.UserId.Equals(userId)).Select(uba => uba.BankAccount).Where(ba => ba.Id.Equals(id)).FirstOrDefault();
        }
    }
}
