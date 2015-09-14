using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.HRMS
{
    [Table("HR_Tickets")]
    public class TicketDef : TableStrutcture
    {
        [Key]
        public int TicketId { get; set; }

        [MaxLength(25)]
        public string TicketCode { get; set; }

        public int Schedule { get; set; }   // e.g. Annual Contract, Annual Year

        public int Eligibility { get; set; }    // e.g. 1+3, 2+1, single

        [MaxLength(500)]
        public String Description { get; set; }

        public short IsActive { get; set; }

    }
}
