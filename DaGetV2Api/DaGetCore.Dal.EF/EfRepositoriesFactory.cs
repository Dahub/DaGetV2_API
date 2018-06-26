using DaGetCore.Dal.Interface;
using Microsoft.EntityFrameworkCore;

namespace DaGetCore.Dal.EF
{
    public class EfRepositoriesFactory : IRepositoriesFactory
    {
        public IContext CreateContext(string connexion)
        {
            var builder = new DbContextOptionsBuilder<DaGetContext>();
            builder.UseSqlServer(connexion, b => b.MigrationsAssembly("DaGetCore.WebApi"));

            return new DaGetContext(builder.Options);
        }

        public IBankAccountRepository GetBankAccountRepository(IContext context)
        {
            return new BankAccountRepository()
            {
                Context = context,
            };
        }

        public IUserBankAccountRepository GetUserBankAccountRepository(IContext context)
        {
            return new UserBankAccountRepository()
            {
                Context = context,
            };
        }

        public IBankAccountOperationTypeRepository GetBankAccountOperationTypeRepository(IContext context)
        {
            return new BankAccountOperationTypeRepository()
            {
                Context = context,
            };
        }
    }
}
