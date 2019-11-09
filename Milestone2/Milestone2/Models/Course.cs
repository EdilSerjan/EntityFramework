using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Milestone2.Models
{
   // IValidatableObject
    public class Course
    {
        public Course()
        {
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Coach Id")]
        public long CoachId { get; set; }

        [Required]
        [DisplayName("Room Id")]
        public long RoomId { get; set; }

        [Required]
        [ForeignKey("CoachId")]
        public Coach Coach { get; set; }

        [Required]
        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        public IList<CourseMember> CourseMembers { get; set; }

    }
}
