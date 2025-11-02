using System.ComponentModel.DataAnnotations;

namespace StudentMVCCoreWb.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number.")]
        public required string MobileNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
