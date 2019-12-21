using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Milestone2.Models
{
    public class Member
    {
        public Member()
        { }
        [Key]
        public long Id { get; set; }

        [Required]
        [NotContainsDigits]
        [Remote(action: "VerifyName", controller: "Members")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public MembershipCard MembershipCard { get; set; }

        public IList<CourseMember> CourseMembers { get; set; }

        public Member(string userName, string email)
        {
            this.Name = userName;
            Email = email;
        }

        public Member(long id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }

    public class NotContainsDigitsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                String stringValue = value.ToString();
                if (stringValue.Any(char.IsDigit) == false)
                    return ValidationResult.Success;     
            }

            return new ValidationResult("Name shouldn't contain digits");
        }
    }
}

