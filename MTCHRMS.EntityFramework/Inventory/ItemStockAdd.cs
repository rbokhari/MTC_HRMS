using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.Inventory
{
    [Table("IV_ItemStockAdd")]
    public class ItemStockAdd : TableStrutcture
    {
        [Key]
        public int ItemStockAddId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [StringLength(50)]
        public string ComputerCode { get; set; }

        [StringLength(100)]
        [Required]
        public string BillNo { get; set; }

        [StringLength(100)]
        [Required]
        public string LpoNo { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public int OrderStock { get; set; }

        [Required]
        public int Stock { get; set; }

        [ForeignKey("SupplierDetail")]
        [Required]
        public int SupplierId { get; set; }

        public int IsWarranty { get; set; }

        public DateTime? WarrantyStart { get; set; }

        public DateTime? WarrantyEnd { get; set; }

        public int IsMaintenance { get; set; }

        public int MaintenanceTypeId { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public virtual Supplier SupplierDetail { get; set; }

        public virtual ICollection<ItemStockSerial> ItemStockSerials { get; set; }

    }
}
