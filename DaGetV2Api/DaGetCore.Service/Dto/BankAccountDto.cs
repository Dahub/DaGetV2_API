using System;
using System.ComponentModel.DataAnnotations;

namespace DaGetCore.Service.Dto
{
    public class BankAccountDto
    {
        public int? Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        [Required(ErrorMessage = "Veuillez spécifier le type de compte BankAccountTypeId")]
        public int? BankAccountTypeId { get; set; }
        public string BankAccountType { get; set; }

        [Required(ErrorMessage = "Veuillez spécifier un nom pour le compte Wording")]
        public string Wording { get; set; }
        public string Number { get; set; }
        public decimal SoldeInitial { get; set; }
        public decimal Solde { get; set; }
        public DateTime? DateSolde { get; set; }

        public string UsersBankAccounts { get; set; }
        public string BankAccountOperationsTypes { get; set; }
        public string ReccurentsOperations { get; set; }
        public string Operations { get; set; }
    }
}
