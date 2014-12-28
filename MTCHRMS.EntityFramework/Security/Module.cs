using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.Security
{
    [Table("AC_Module")]
    public class AC_Module : TableStrutcture
    {
        [Key]
        public int ModuleId { get; set; }


        public string ModuleName { get; set; }

        public string ModuleNameAr { get; set; }


    }
}
