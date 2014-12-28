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
        public string DepartmentName { get; set; }
        public int StatusId { get; set; }

    }
}
