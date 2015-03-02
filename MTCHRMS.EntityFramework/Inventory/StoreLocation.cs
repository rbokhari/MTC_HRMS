using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.Inventory
{
    [Table("IV_Store")]
    public class StoreLocation : TableStrutcture
    {
        [Key]
        public int StoreId { get; set; }

        public string StoreCode { get; set; }

        public string StoreName { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; }
    }
}
