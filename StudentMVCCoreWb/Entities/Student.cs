using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMVCCoreWb.Entities
{
    public class Student
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

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

    }
}
