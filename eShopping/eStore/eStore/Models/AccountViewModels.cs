using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eStore.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        //[EmailAddress]
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
        [Required(ErrorMessage = "Plotësoni fushën!")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Plotësoni fushën!")]
        
        [Display(Name = "UserName")]
        //[System.Web.Mvc.Remote("checkUser", "USERs")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Plotësoni fushën!")]
        [StringLength(100, ErrorMessage = "Fjalëkalimi duhet të jetë minimum {2} karaktere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Fjalëkalimi dhe konfirmo fjalëkalimin nuk përputhen.")]
        [Required(ErrorMessage = "Plotësoni fushën!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Plotësoni fushën!")]
        public string Emri { get; set; }

        [Required(ErrorMessage = "Plotësoni fushën!")]
        public string Mbiemri { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Ditelindja { get; set; }

        [Required(ErrorMessage = "Plotësoni fushën!")]
        public string Adresa { get; set; }

        public string Telefoni { get; set; }
        public string KodiPostar { get; set; }
        public bool Aktiv { get; set; }

        [Required(ErrorMessage = "Zgjedhni rolin!")]
        public int Roles { get; set; }
      
        [Required(ErrorMessage = "Zgjedhni gjuhen!")]
        public int GjuhaID { get; set; }

        public PERDORUESI u { get; set; }
      
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
