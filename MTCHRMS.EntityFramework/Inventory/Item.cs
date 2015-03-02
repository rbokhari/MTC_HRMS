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
    [Table("IV_Item")]
    public class Item : TableStrutcture
    {
        [Key]
        public int ItemId { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public int TypeId { get; set; }

        public string Utilization { get; set; }

        public int StoreId { get; set; }

        public decimal ItemStock { get; set; }

        public string Notes { get; set; }


    }
}
