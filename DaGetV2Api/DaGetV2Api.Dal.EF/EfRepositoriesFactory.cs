using DaGetV2Api.Dal.Interface;

namespace DaGetV2Api.Dal.EF
{
    public class EfRepositoriesFactory : IRepositoriesFactory
    {
        public IContext CreateContext(string connexion)
        {
            return new DaGetContext(connexion);
        }

        public IBankAccountRepository GetBankAccountRepository(IContext context)
        {
            return new BankAccountRepository()
            {
                Context = context,
            };
        }
    }
}
