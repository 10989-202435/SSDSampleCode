using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace L04Cryptography.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [ReadOnly(true)]
        public string? EncryptedNumber { get; set; }
        [ReadOnly(true)]
        public string? DecryptedNumber { get; set; }
    }
}
