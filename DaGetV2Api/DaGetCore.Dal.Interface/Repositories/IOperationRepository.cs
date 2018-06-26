using DaGetCore.Domain;
using System.Collections.Generic;

namespace DaGetCore.Dal.Interface
{
    public interface IOperationRepository : IRepository<Operation>
    {
        IEnumerable<Operation> GetAllByBankAccountId(int bankAccountId);
    }
}
