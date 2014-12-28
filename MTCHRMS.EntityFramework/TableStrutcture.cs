using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework
{
    public class TableStrutcture
    {
        public string MachineName { get; set; }
        public string MachineUser { get; set; }
        public int CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
