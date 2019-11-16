using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Milestone2.Models
{
    public class Coach
    {
        public Coach()
        {
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        
        public Course Course { get; set; }

    }
}
