using DaGetCore.Constants;
using DaGetCore.Domain;
using DaGetCore.Service.Dto;

namespace DaGetCore.Service.Tools
{
    internal static class ExtensionMethods
    {
        internal static BankAccountDto ToDto(this BankAccount ba)
        {
            return new BankAccountDto()
            {
                BankAccountOperationsTypes = string.Format("/{0}/{1}/{2}", Routes.bankAccount, ba.Id, Routes.operationType),
                BankAccountType = string.Format("/{0}/{1}/{2}", Routes.bankAccount, ba.Id, Routes.bankAccountType),
                Id = ba.Id,
                BankAccountTypeId = ba.BankAccountTypeId,
                CreationDate = ba.CreationDate,
                DateSolde = ba.DateSolde,
                ModificationDate = ba.ModificationDate,
                Number = ba.Number,
                Operations = string.Format("/{0}/{1}/{2}",Routes.bankAccount, ba.Id, Routes.operation),
                ReccurentsOperations = string.Format("/{0}/{1}/{2}", Routes.bankAccount, ba.Id, Routes.reccurentOperation),
                Solde = ba.Solde,
                SoldeInitial = ba.SoldeInitial,
                UsersBankAccounts = string.Format("/{0}/{1}/{2}", Routes.bankAccount, ba.Id, Routes.user),
                Wording = ba.Wording
            };
        }
    }
}
