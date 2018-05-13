using DaGetV2Api.Dal.Interface;
using DaGetV2Api.Domain;
using System.Data.Entity;

namespace DaGetV2Api.Dal.EF
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
