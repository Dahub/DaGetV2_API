using System;
using System.Collections.Generic;

namespace DaGetCore.Domain
{
    public class BankAccount : DomainObjectBase
    {
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int BankAccountTypeId { get; set; }
        public BankAccountType BankAccountType { get; set; }
        public string Wording { get; set; }
        public string Number { get; set; }
        public decimal SoldeInitial { get; set; }
        public decimal Solde { get; set; }
        public DateTime DateSolde { get; set; }
        public bool HadApplyCurrentOperationsThisMonth { get; set; }

        public ICollection<UserBankAccount> UsersBankAccounts { get; set; }
        public ICollection<BankAccountOperationType> BankAccountOperationsTypes { get; set; }
        public ICollection<ReccurentOperation> ReccurentsOperations { get; set; }
        public ICollection<Operation> Operations { get; set; }
    }
}
