using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DaGetCore.Dal.EF
{
    internal class BankAccountOperationTypeRepository : RepositoryBase<BankAccountOperationType>, IBankAccountOperationTypeRepository
    {
        public IEnumerable<BankAccountOperationType> GetAllByBankAccountId(int bankAccountId)
        {
            return ((DaGetContext)Context).BankAccountOperationsTypes.Where(bao => bao.BankAccountId.Equals(bankAccountId));
        }
    }
}
