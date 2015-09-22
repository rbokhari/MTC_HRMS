using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.HRMS
{
    [Table("HR_Leaves")]
    public class LeaveDef : TableStrutcture
    {
        [Key]
        public int LeaveId { get; set; }

        [MaxLength(25)]
        [Required]
        public String Code { get; set; }

        [MaxLength(255)]
        [Required]
        public String LeaveName { get; set; }

        [Required]
        public int Type { get; set; }  // e.g. Annual leave, emergency leave, sick leave

        [Required]
        public int Total { get; set; }

        [Required]
        public int Schedule { get; set; }   // e.g. Annual Contract, Annual Year

        [MaxLength(500)]
        public String Description { get; set; }

        [Required]
        public short IsActive { get; set; }

    }
}
