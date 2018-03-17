using System;
using System.ComponentModel.DataAnnotations;

namespace INTEC.Data
{
    public class BaseEntity
    {
        [Key]
        public Int32 Id { get; set; }
        public String RowId { get; set; }
        public DateTime Created { get; set; }
        public Int32 CreatedByUserId { get; set; }
        public DateTime? Modified { get; set; }
        public Int32? ModifiedByUserId { get; set; }
        public DateTime? Deleted { get; set; }
        public Int32? DeletedByUserId { get; set; }

        [Required]
        public String Enrollment { get; set; }
    }
}
