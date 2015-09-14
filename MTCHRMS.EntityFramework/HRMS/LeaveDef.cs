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
        public String Code { get; set; }

        public int Type { get; set; }  // e.g. Annual leave, emergency leave, sick leave

        public int Total { get; set; }

        public int Schedule { get; set; }   // e.g. Annual Contract, Annual Year

        [MaxLength(500)]
        public String Description { get; set; }

        public short IsActive { get; set; }

    }
}
