using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrystalCarCare.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(\+88)?01[3-9]\d{8}$", ErrorMessage = "Invalid Bangladeshi phone number format")]
        public string Phone { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        [Display(Name = "Bio")]
        [StringLength(500, ErrorMessage = "Bio cannot exceed 500 characters")]
        public string Bio { get; set; }
    }
}