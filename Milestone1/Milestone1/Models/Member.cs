using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Milestone1.Models
{
    public class Member
    {
        public Member()
        { }
        [Key]
        public long id { get; set; }

        public string name { get; set; }

        public string telephone { get; set; }

        public MembershipCard membershipCard { get; set; }

        public IList<Schedule> schedules { get; set; }
    }
}

