using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.EntityFramework
{
    public class TableStrutcture
    {
        [MaxLength(50)]
        public string MachineName { get; set; }

        [MaxLength(50)]
        public string MachineUser { get; set; }

        //[ForeignKey("CreatedDetail")]
        public int CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; }

        //[ForeignKey("ModifiedDetail")]
        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }


        //public virtual EmployeeDef CreatedDetail { get; set; }

        //public virtual EmployeeDef ModifiedDetail { get; set; }
    }
}
