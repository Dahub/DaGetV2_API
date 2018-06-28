using DaGetCore.Dal.Interface;
using DaGetCore.Domain;
using DaGetCore.Service.Tools;
using System;

namespace DaGetCore.Service
{
    public abstract class ServiceBase
    {
        public IRepositoriesFactory Factory { get; set; }
        public string ConnexionString { get; set; }

        protected BankAccount ExtractBankAccount(Guid? userId, int bankAccountId, IContext context)
        {
            IBankAccountRepository baRepo = Factory.GetBankAccountRepository(context);

            var ba = baRepo.GetByIdUserAndId(userId.Value, bankAccountId);
            
            if (ba == null) // compte inexistant ou n'appartenant pas à cet utilisateur
            {
                throw new DaGetServiceException("Compte inexistant ou vous n'avez pas l'autorisation d'y accéder");
            }

            return ba;
        }
    }
}
    