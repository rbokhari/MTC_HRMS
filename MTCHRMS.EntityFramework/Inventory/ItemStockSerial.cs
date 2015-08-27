using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.EntityFramework.Inventory
{
    [Table("IV_ItemStockSerial")]
    public class ItemStockSerial : TableStrutcture
    {
        [Key]
        public int ItemStockSerialId { get; set; }

        public int ItemStockAddId { get; set; }

        public int ItemId { get; set; }

        [StringLength(250)]
        public string SerialNo { get; set; }

        public int BarcodePrintCount { get; set; }

        [ForeignKey("ItemStockStatusDetail")]
        public int StatusId { get; set; }

        public virtual ValidationDetail ItemStockStatusDetail { get; set; }

        public virtual DistributionItem DistributionItem { get; set; }
    }

}
