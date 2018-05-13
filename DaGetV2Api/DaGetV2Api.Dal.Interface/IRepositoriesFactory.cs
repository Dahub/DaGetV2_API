namespace DaGetV2Api.Dal.Interface
{
    public interface IRepositoriesFactory
    {
        IContext CreateContext(string connexion);
        IBankAccountRepository GetBankAccountRepository(IContext context);
    }
}
