using DaGetCore.Domain;
using DaGetCore.Service.Dto;
using DaGetCore.Service.Tools;
using System;

namespace DaGetCore.Service
{
    public class BankAccountService : ServiceBase, IBankAccountService
    {
        public BankAccountDto Create(Guid? userId, string userName, BankAccountDto toCreate)
        {
            try
            {
                using (var context = Factory.CreateContext(ConnexionString))
                {
                    // création du compte
                    var baRepo = Factory.GetBankAccountRepository(context);
                    BankAccount bankAccountToAdd = new BankAccount()
                    {
                        BankAccountTypeId = toCreate.BankAccountTypeId.Value,
                        CreationDate = DateTime.Now,
                        DateSolde = DateTime.Now,
                        ModificationDate = DateTime.Now,
                        Number = toCreate.Number,
                        Solde = 0M,
                        SoldeInitial = toCreate.SoldeInitial,
                        Wording = toCreate.Wording                        
                    };
                    baRepo.Add(bankAccountToAdd);

                    // création de l'user
                    var ubaRepo = Factory.GetUserBankAccountRepository(context);
                    UserBankAccount userBankAccountToAdd = new UserBankAccount()
                    {
                        BankAccountId = bankAccountToAdd.Id,
                        BankAccountAccessId = 1,
                        UserId = userId.Value,
                        UserName = userName
                    };
                    ubaRepo.Add(userBankAccountToAdd);

                    context.Commit();                    
                }
            }
            catch (Exception ex)
            {
                throw new DaGetServiceException(
                    String.Format("Erreur lors de création du compte pour l'utilisateur {0}", userId), ex);
            }

            return toCreate;
        }

        public BankAccountDto GetById(Guid? userId, int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
