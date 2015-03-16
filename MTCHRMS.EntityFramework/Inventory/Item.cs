using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.EntityFramework.Inventory
{
    [Table("IV_Item")]
    public class Item : TableStrutcture
    {
        [Key]
        public int ItemId { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        [ForeignKey("TypeDetail")]
        public int TypeId { get; set; }

        [ForeignKey("CategoryDetail")]
        public int CategoryId { get; set; }

        public string Utilization { get; set; }

        [ForeignKey("StoreLocation")]
        public int StoreId { get; set; }

        public decimal ItemStock { get; set; }

        public string Notes { get; set; }

        public int IsIT { get; set; }

        public int IsCallibration { get; set; }

        public int IsMaintenance { get; set; }

        public int TechnicanTypeId { get; set; }

        public string Spares { get; set; }

        public int? StatusId { get; set; }


        public virtual ValidationDetail TypeDetail { get; set; }

        public virtual ValidationDetail CategoryDetail { get; set; }

        public virtual StoreLocation StoreLocation { get; set; }
    }
}
