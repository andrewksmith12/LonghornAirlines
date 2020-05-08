using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
namespace LonghornAirlines.Models.Users
{ 
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        //TODO: Add any fields that you need for creating a new user
        //First name is provided as an example
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name: ")]
        public String LastName { get; set; }

        [Display(Name = "Middle Initial: ")]
        [StringLength(1, MinimumLength = 0, ErrorMessage = "Middle Initial must be between 0 and 1 characters")]
        public String MI { get; set; }

        [Required]
        [Display(Name = "Birthday: ")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Advantage Number: ")]
        public String AdvantageNumber { get; set; }

        [Required]
        [Display(Name = "Street: ")]
        public String Street { get; set; }

        [Required]
        [Display(Name = "City: ")]
        public String City { get; set; }

        [Required]
        [Display(Name = "State: ")]
        public String State { get; set; }

        [Required]
        [Display(Name = "Zip Code: ")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "ZIP Code must be 5 characters")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "ZIP Code must only contain numbers")]
        public String ZIP { get; set; }

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //NOTE: Here is the property for phone number
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Social Security Number")]
        public String SSN { get; set; }

        [Display(Name = "Enter the employee's Role (Must exist in db)")]
        public String Role { get; set; }

        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }   

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String UserID { get; set; }
    }
}
