using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework
{
    public class TableStrutcture
    {
        [MaxLength(50)]
        public string MachineName { get; set; }

        [MaxLength(50)]
        public string MachineUser { get; set; }
        public int CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
