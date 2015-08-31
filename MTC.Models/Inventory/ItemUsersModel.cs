using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTC.Models.Inventory
{
    public class ItemUsersModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string DesignationName { get; set; }


    }
}
