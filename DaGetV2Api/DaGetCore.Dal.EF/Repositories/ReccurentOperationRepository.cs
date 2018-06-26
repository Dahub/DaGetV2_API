using System.Collections.Generic;
using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using System.Linq;

namespace DaGetCore.Dal.EF
{
    internal class ReccurentOperationRepository : RepositoryBase<ReccurentOperation>, IReccurentOperationRepository
    {
        public IEnumerable<ReccurentOperation> GetAllByBankAccountId(int bankAccountId)
        {
            return ((DaGetContext)Context).ReccurentsOperations.Where(ro => ro.BankAccountId.Equals(bankAccountId));
        }
    }
}
