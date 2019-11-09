using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Milestone2.Models
{
    public class MembershipCard
    {
        public MembershipCard()
        {
        }
        [Key]
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [DisplayName("Member Id")]
        public long MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }
    }
}
