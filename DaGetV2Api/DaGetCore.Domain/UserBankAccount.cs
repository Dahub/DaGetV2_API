using System;

namespace DaGetCore.Domain
{
    public class UserBankAccount : DomainObjectBase
    {
        public string UserName { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        public int BankAccountAccessId { get; set; }
        public BankAccountAccess BankAccountAccess { get; set; }
        public Guid UserId { get; set; }
    }
}
