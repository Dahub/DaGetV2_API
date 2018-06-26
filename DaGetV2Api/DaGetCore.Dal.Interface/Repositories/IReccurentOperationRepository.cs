using DaGetCore.Domain;
using System.Collections.Generic;

namespace DaGetCore.Dal.Interface
{
    public interface IReccurentOperationRepository : IRepository<ReccurentOperation>
    {
        IEnumerable<ReccurentOperation> GetAllByBankAccountId(int bankAccountId);
    }
}
