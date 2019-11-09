using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Milestone2.Models
{
    public class Room
    {
        public Room()
        {
        }
        [Key]
        public long Id { get; set; }

        [Required]
        [Range(10,50)]
        public int Capcity { get; set; }

        public IList<Course> Courses { get; set; }

        public IList<Equipment> Equipments { get; set; }
    }
}
