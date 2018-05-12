﻿using System.Collections.Generic;

namespace DaGetV2Api.Domain
{
    public class BankAccountOperationType
    {
        public int Id { get; set; }
        public string Wording { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        
        public ICollection<Operation> Operations { get; set; }
    }
}
