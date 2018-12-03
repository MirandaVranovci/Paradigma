using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace eStore.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
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

    public class ChangePasswordViewModel
    {
        public int ID { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Fjalëkalimi duhet të jetë minimum {2} karaktere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Fjalëkalimi i ri")]
        [System.Web.Mvc.Remote("CheckPassword", "USER")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmo fjalëkalimin")]
        [Compare("NewPassword", ErrorMessage = "Fjalëkalimi dhe konfirmo fjalëkalimin nuk përputhen.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

    public class EditViewModel
    {
        public EditViewModel()
        {
            psw = new ChangePasswordViewModel();
            user = new PERDORUESI();
        }

        public ChangePasswordViewModel psw { get; set; }
        public PERDORUESI user { get; set; }
        public int ID { get; set; }

        [Required]
        [System.Web.Mvc.Remote("CheckUserName", "USER", AdditionalFields = "ID")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}