using System.Collections.Generic;

namespace DaGetCore.Domain
{
    public class BankAccountOperationType
    {
        public int Id { get; set; }
        public string Wording { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        
        public ICollection<Operation> Operations { get; set; }
        public ICollection<ReccurentOperation> ReccurentsOperations { get; set; }
    }
}
