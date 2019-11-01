using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Milestone1.Models
{
    public class Equipment
    {
        public Equipment()
        {
        }
        [Key]
        public long id { get; set; }

        public string name { get; set; }

        public int price { get; set; }

        public long roomId { get; set; }

        [ForeignKey("roomId")]
        public Room room { get; set; }

    }
}
