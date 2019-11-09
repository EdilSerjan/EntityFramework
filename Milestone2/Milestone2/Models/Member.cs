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
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Members")]
        public string Email { get; set; }

        public MembershipCard MembershipCard { get; set; }

        public IList<CourseMember> CourseMembers { get; set; }
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

