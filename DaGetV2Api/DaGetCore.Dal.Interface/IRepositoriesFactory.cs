namespace DaGetCore.Dal.Interface
{
    public interface IRepositoriesFactory
    {
        IContext CreateContext(string connexion);
        IBankAccountRepository GetBankAccountRepository(IContext context);
        IUserBankAccountRepository GetUserBankAccountRepository(IContext context);
        IBankAccountOperationTypeRepository GetBankAccountOperationTypeRepository(IContext context);
    }
}
