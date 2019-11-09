using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Milestone2.Models
{
    public class CourseMember
    {
        public CourseMember()
        {
        }
        //[Key]
        //public long id { get; set; }

        [Required]
        [DisplayName("Course Id")]
        public long CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [Required]
        [DisplayName("Member Id")]
        public long MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }


        [Required]
        public String Day { get; set; }


  
    }
}
