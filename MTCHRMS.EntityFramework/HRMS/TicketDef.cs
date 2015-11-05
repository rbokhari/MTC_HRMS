using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.EntityFramework.HRMS
{
    [Table("HR_Tickets")]
    public class TicketDef : TableStrutcture
    {
        [Key]
        public int TicketId { get; set; }

        [MaxLength(25)]
        public string TicketCode { get; set; }

        [MaxLength(255)]
        [Required]
        public String TicketName { get; set; }

        [Required]
        [ForeignKey("ScheduleDetail")]
        public int Schedule { get; set; }   // e.g. Annual Contract, Annual Year

        [Required]
        [ForeignKey("EligibilityDetail")]
        public int Eligibility { get; set; }    // e.g. 1+3, 2+1, single

        [MaxLength(500)]
        public String Description { get; set; }

        [Required]
        public short IsActive { get; set; }

        public virtual ValidationDetail ScheduleDetail { get; set; }

        public virtual ValidationDetail EligibilityDetail { get; set; }



    }
}
