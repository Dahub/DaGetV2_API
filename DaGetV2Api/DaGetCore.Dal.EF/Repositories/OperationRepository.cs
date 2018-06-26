using System.Collections.Generic;
using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using System.Linq;

namespace DaGetCore.Dal.EF
{
    internal class OperationRepository : RepositoryBase<Operation>, IOperationRepository
    {
        public IEnumerable<Operation> GetAllByBankAccountId(int bankAccountId)
        {
            return ((DaGetContext)Context).Operations.Where(o => o.BankAccountId.Equals(bankAccountId));
        }
    }
}
