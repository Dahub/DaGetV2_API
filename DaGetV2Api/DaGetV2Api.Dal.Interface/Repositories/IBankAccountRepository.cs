using DaGetV2Api.Domain;

namespace DaGetV2Api.Dal.Interface
{
    public interface IBankAccountRepository : IRepository
    {
        void Add(BankAccount toAdd);
    }
}
