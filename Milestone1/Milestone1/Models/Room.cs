using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Milestone1.Models
{
    public class Room
    {
        public Room()
        {
        }
        [Key]
        public long id { get; set; }

        public int capcity { get; set; }

        public IList<Course> courses { get; set; }

        public IList<Equipment> equipments { get; set; }
    }
}
