using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace Milestone2.Models
{
    public class Equipment : IValidatableObject
    {
        public Equipment()
        {
        }
        [Key]
        public long Id { get; set; }

        [StringLength(20,ErrorMessage ="Name length should be less than 20")]
        [Required]
        public string Name { get; set; }

        [Range(0, 50000)]
        [Required]
        public int Price { get; set; }

        [Required]
        [DisplayName("Room Id")]
        public long RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.ToLower() == "bench" && Price > 20000)
            {
                yield return new ValidationResult("Bench shouldn't cost more than 20000");
            }
        }
    }
}
