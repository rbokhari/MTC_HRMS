using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.Security
{
    [Table("AC_User")]
    public class AC_User : TableStrutcture
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int DepartmentId { get; set; }

        public int PositionId { get; set; }

        public int RoleId { get; set; }

        public int ModuleId { get; set; }

        public string CurrLang { get; set; }

        public int EmployeeId { get; set; }

    }
}
