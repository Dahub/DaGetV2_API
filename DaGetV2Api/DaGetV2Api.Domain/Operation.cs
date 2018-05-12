﻿using System;
using System.Collections.Generic;

namespace DaGetV2Api.Domain
{
    public class Operation
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int BankAccountOperationTypeId { get; set; }
        public BankAccountOperationType BankAccountOperationType { get; set; }

        public int? ParentOperationId { get; set; }
        public Operation ParentOperation { get; set; }
        public ICollection<Operation> ChildOperations { get; set; }

        public DateTime OperationDate { get; set; }
        public bool Closed { get; set; }
        public decimal Amount { get; set; }
    }
}
