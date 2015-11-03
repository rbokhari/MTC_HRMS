using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.EntityFramework.HRMS
{
    [Table("HR_Leaves")]
    public class LeaveDef : TableStrutcture
    {
        [Key]
        public int LeaveId123 { get; set; }

        [MaxLength(25)]
        public String Code { get; set; }

        [MaxLength(255)]
        [Required]
        public String LeaveName { get; set; }

        [Required]
        [ForeignKey("TypeDetail")]
        public int TypeId { get; set; }  // e.g. Annual leave, emergency leave, sick leave

        [Required]
        public int Total { get; set; }

        [Required]
        [ForeignKey("ScheduleDetail")]
        public int ScheduleId { get; set; }   // e.g. Annual Contract, Annual Year

        [MaxLength(500)]
        public String Description { get; set; }

        [Required]
        public short IsActive { get; set; }

        public virtual ValidationDetail TypeDetail { get; set; }

        public virtual ValidationDetail ScheduleDetail { get; set; }

    }
}
