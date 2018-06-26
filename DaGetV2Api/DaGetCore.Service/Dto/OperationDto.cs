using System;
using System.ComponentModel.DataAnnotations;

namespace DaGetCore.Service.Dto
{
    public class OperationDto
    {
        public int? Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        [Required(ErrorMessage = "Veuillez spécifier le compte BankAccountId")]
        public int? BankAccountId { get; set; }
        public string BankAccount { get; set; }

        [Required(ErrorMessage = "Veuillez spécifier le type d'opération BankAccountOperationTypeId")]
        public int? BankAccountOperationTypeId { get; set; }
        public string BankAccountOperationType { get; set; }

        public int? ParentOperationId { get; set; }
        public string ParentOperation { get; set; }

        [Required(ErrorMessage = "Veuillez spécifier la date de l'opération OperationDate")]
        public DateTime? OperationDate { get; set; }

        [Required(ErrorMessage = "Veuillez indiquer si l'opération est close Closed")]
        public bool? Closed { get; set; }

        [Required(ErrorMessage = "Veuillez indiquer le montant de l'opération Amount")]
        public decimal? Amount { get; set; }
    }
}
