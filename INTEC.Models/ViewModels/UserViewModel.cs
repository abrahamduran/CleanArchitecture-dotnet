using System;
using System.ComponentModel.DataAnnotations;

namespace INTEC.Models.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public String DisplayName { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean Locked { get; set; }
        public DateTime? LockedDate { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }

        [Display(Name = "Recordar Credenciales")]
        public Boolean RememberMe { get; set; }
    }
}
