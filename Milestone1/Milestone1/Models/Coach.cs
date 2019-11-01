using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Milestone1.Models
{
    public class Coach
    {
        public Coach()
        {
        }

        [Key]
        public long id { get; set; }
        public string name { get; set; }
        public string tel { get; set; }

        public Course program { get; set; }

    }
}
