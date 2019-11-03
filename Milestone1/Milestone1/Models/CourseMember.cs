using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Milestone1.Models
{
    public class CourseMember
    {
        public CourseMember()
        {
        }
        //[Key]
        //public long id { get; set; }

        public long courseId { get; set; }
        [ForeignKey("courseId")]
        public Course course { get; set; }


        public long memberId { get; set; }
        [ForeignKey("memberId")]
        public Member member { get; set; }

        public string day { get; set; }

    }
}
