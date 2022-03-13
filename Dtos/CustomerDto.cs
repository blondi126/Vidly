using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Required]
        public byte? MembershipTypeId { get; set; }

        public MembershipTypeDto? MembershipType { get; set; }

        [Min18YearsIfAMemberDto]
        public DateTime? Birthday { get; set; }
    }
}
