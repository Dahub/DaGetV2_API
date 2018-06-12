using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace DaGetCore.Dal.EF
{
    internal class BankAccountRepository : IBankAccountRepository
    {
        public IContext Context { get; set; }

        public void Add(BankAccount toAdd)
        {
            ((DbContext)Context).Set<BankAccount>().Add(toAdd);
        }
    }
}
