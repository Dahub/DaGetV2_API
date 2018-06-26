using DaGetCore.Domain;
using System.Collections.Generic;

namespace DaGetCore.Dal.Interface
{
    public interface IBankAccountOperationTypeRepository : IRepository<BankAccountOperationType>
    {
        IEnumerable<BankAccountOperationType> GetAllByBankAccountId(int bankAccountId);
    }
}
