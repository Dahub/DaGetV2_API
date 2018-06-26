using System;

namespace DaGetCore.Domain
{
    public class ReccurentOperation : DomainObjectBase
    {
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        public int BankAccountOperationTypeId { get; set; }
        public BankAccountOperationType BankAccountOperationType { get; set; }
        public int OperationDayOfMonth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public bool January { get; set; }
        public bool February { get; set; }
        public bool March { get; set; }
        public bool April { get; set; }
        public bool May { get; set; }
        public bool June { get; set; }
        public bool July { get; set; }
        public bool August { get; set; }
        public bool September { get; set; }
        public bool October { get; set; }
        public bool November { get; set; }
        public bool December { get; set; }
    }
}
