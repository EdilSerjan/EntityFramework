using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Milestone1.Models
{
    public class MembershipCard
    {
        public MembershipCard()
        {
        }
        [Key]
        public long id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime createdAt { get; set; }

        public long memberId { get; set; }

        [ForeignKey("memberId")]
        public Member member { get; set; }
    }
}
