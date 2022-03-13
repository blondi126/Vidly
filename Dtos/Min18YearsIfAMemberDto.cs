using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos

{
    public class Min18YearsIfAMemberDto : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var customer = (CustomerDto)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;
            if (customer.Birthday == null)
                return new ValidationResult("Birthday is required.");
            var age = DateTime.Today.Year - customer.Birthday.Value.Year;
            if (age < 18)
                return new ValidationResult("Customer should be at least 18 years old to go on a membership.");
            else
                return ValidationResult.Success;

        }
    }
}
