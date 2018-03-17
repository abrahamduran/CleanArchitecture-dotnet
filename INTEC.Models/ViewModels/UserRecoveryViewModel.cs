using System;
namespace INTEC.Models.ViewModels
{
    public class UserRecoveryViewModel : BaseViewModel
    {
        public Int32 UserId { get; set; }
        public String Token { get; set; }
        public Boolean Used { get; set; }
        public DateTime? UsedDate { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
