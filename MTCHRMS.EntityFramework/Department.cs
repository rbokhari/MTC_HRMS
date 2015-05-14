using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework
{
    public class Department : TableStrutcture
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Department Code")]
        public string DepartmentCode { get; set; }

        [Required(ErrorMessage = "Please enter Department Name")]
        [MaxLength(300)]
        public string DepartmentName { get; set; }

        [MaxLength(300)]
        public String DepartmentNameAr { get; set; }
        public int StatusId { get; set; }

    }
}
