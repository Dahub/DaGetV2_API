using DaGetCore.Domain;

namespace DaGetCore.Dal.Interface
{
    public interface IBankAccountRepository : IRepository
    {
        void Add(BankAccount toAdd);
    }
}
