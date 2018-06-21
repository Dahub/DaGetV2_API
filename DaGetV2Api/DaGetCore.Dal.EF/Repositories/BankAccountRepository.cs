using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DaGetCore.Dal.EF
{
    internal class BankAccountRepository : IBankAccountRepository
    {
        public IContext Context { get; set; }

        public void Add(BankAccount toAdd)
        {
            ((DbContext)Context).Set<BankAccount>().Add(toAdd);
        }

        public void Delete(BankAccount toDelete)
        {
            ((DbContext)Context).Set<BankAccount>().Attach(toDelete);
            ((DbContext)Context).Entry(toDelete).State = EntityState.Deleted;
        }

        public IEnumerable<BankAccount> GetAllByIdUser(Guid userId)
        {
            return ((DaGetContext)Context).UsersBankAccounts.Where(uba => uba.UserId.Equals(userId)).Select(uba => uba.BankAccount);
        }

        public BankAccount GetAllByIdUserAndId(Guid userId, int id)
        {
            return ((DaGetContext)Context).UsersBankAccounts.Where(uba => uba.UserId.Equals(userId)).Select(uba => uba.BankAccount).Where(ba => ba.Id.Equals(id)).FirstOrDefault();
        }

        public void Update(BankAccount toUpdate)
        {
            ((DbContext)Context).Set<BankAccount>().Attach(toUpdate);
            ((DbContext)Context).Entry(toUpdate).State = EntityState.Modified;
        }
    }
}
