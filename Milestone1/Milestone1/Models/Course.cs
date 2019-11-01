using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Milestone1.Models
{
    public class Course
    {
        public Course()
        {
        }

        [Key]
        public long id { get; set; }

        public string name { get; set; }

        public long coachId { get; set; }

        public long roomId { get; set; }

        [ForeignKey("coachId")]
        public Coach coach { get; set; }

        [ForeignKey("roomId")]
        public Room room { get; set; }

        public IList<Schedule> schedules { get; set; }


    }
}
