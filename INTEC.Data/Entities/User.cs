using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTEC.Data.Entities
{
    [Table("User", Schema = "dbo")]
    public class User : BaseEntity
    {
        [Required]
        public String Username { get; set; }

        [Required]
        [MaxLength(50)]
        public String Password { get; set; }

        [MaxLength(254)]
        public String Email { get; set; }

        public DateTime? LastAccessDate { get; set; }
        public String DisplayName { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean Locked { get; set; }
        public DateTime? LockedDate { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }
    }
}
