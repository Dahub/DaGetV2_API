using DaGetCore.Constants;
using DaGetCore.Domain;
using DaGetCore.Service.Dto;

namespace DaGetCore.Service.Tools
{
    internal static class ExtensionMethods
    {
        internal static BankAccountTypeDto ToDto(this BankAccountType bat)
        {
            return new BankAccountTypeDto()
            {
                Id = bat.Id,
                Wording = bat.Wording
            };
        }

        internal static OperationDto ToDto(this Operation o)
        {
            return new OperationDto()
            {
                Amount = o.Amount,
                BankAccountId = o.BankAccountId,
                Closed = o.Closed,
                CreationDate = o.CreationDate,
                Id = o.Id,
                ModificationDate = o.ModificationDate,
                OperationDate = o.OperationDate,
                BankAccountOperationTypeId = o.BankAccountOperationTypeId,
                ParentOperationId = o.ParentOperationId,
                BankAccount = string.Format("/{0}/{1}", Routes.bankAccount, o.BankAccountId),
                BankAccountOperationType = string.Format("/{0}/{1}/{2}/{3}/{4}", Routes.bankAccount, o.BankAccountId, Routes.operation, o.Id, Routes.operationType),
                ParentOperation = o.ParentOperationId.HasValue ? string.Format("/{0}/{1}/{2}/{3}", Routes.bankAccount, o.BankAccountId, Routes.operation, o.ParentOperationId) : string.Empty
            };
        }

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
                Operations = string.Format("/{0}/{1}/{2}", Routes.bankAccount, ba.Id, Routes.operation),
                ReccurentsOperations = string.Format("/{0}/{1}/{2}", Routes.bankAccount, ba.Id, Routes.reccurentOperation),
                Solde = ba.Solde,
                SoldeInitial = ba.SoldeInitial,
                UsersBankAccounts = string.Format("/{0}/{1}/{2}", Routes.bankAccount, ba.Id, Routes.user),
                Wording = ba.Wording
            };
        }
    }
}
