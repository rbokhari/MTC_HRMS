using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.Security
{
    [Table("AC_Role")]
    public class AC_Role : TableStrutcture
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

    }
}
