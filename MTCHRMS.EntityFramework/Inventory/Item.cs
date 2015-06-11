using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.EntityFramework.Inventory
{
    [Table("IV_Item")]
    public class Item : TableStrutcture
    {
        public Item()
        {
            ItemDepartments = new Collection<ItemDepartment>();
        }

        [Key]
        public int ItemId { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string NatoNo { get; set; }

        public string SerialNo { get; set; }

        [ForeignKey("TypeDetail")]
        public int TypeId { get; set; }

        [ForeignKey("CategoryDetail")]
        public int CategoryId { get; set; }

        public string Utilization { get; set; }

        [ForeignKey("StoreLocation")]
        public int StoreId { get; set; }

        public decimal ItemStock { get; set; }

        public string Notes { get; set; }

        public int IsIt { get; set; }

        public int IsCallibration { get; set; }

        public int IsMaintenance { get; set; }

        [ForeignKey("TechnicianType")]
        public int TechnicanTypeId { get; set; }

        public string Spares { get; set; }

        public int? StatusId { get; set; }

        [Column(TypeName = "image")]
        public byte[] ItemPicture { get; set; }

        [NotMapped]
        public int Condition { get; set; }

        [NotMapped]
        public int DepartmentId { get; set; }

        [NotMapped]
        public int SupplierId { get; set; }

        [NotMapped]
        public int ManufacturerId { get; set; }


        public virtual ValidationDetail TypeDetail { get; set; }

        public virtual ValidationDetail CategoryDetail { get; set; }

        public virtual ValidationDetail TechnicianType { get; set; }

        public virtual StoreLocation StoreLocation { get; set; }

        public virtual ItemProvision ItemProvision { get; set; }

        public virtual ICollection<ItemDepartment> ItemDepartments { get; set; }

        public virtual ICollection<ItemYear> ItemYears { get; set; }

        public virtual ICollection<ItemSupplier> ItemSuppliers { get; set; }

        public virtual ICollection<ItemManufacturer> ItemManufacturers { get; set; }
    }

    [Table("IV_ItemDepartment")]
    public class ItemDepartment : TableStrutcture
    {
        [Key]
        public int ItemDepartmentId { get; set; }

        public int ItemId { get; set; }

        [ForeignKey("DepartmentDetail")]
        public int DepartmentId { get; set; }

        public string Notes { get; set; }

        public virtual Department DepartmentDetail { get; set; }
    }


    [Table("IV_ItemYear")]
    public class ItemYear : TableStrutcture
    {
        [Key]
        public int ItemYearId { get; set; }

        public int ItemId { get; set; }

        [ForeignKey("YearDetail")]
        public int YearId { get; set; }

        public string Notes { get; set; }

        public virtual ValidationDetail YearDetail { get; set; }
    }

    [Table("IV_ItemSupplier")]
    public class ItemSupplier : TableStrutcture
    {
        [Key]
        public int ItemSupplierId { get; set; }

        public int ItemId { get; set; }

        [ForeignKey("SupplierDetail")]
        public int SupplierId { get; set; }

        public string Notes { get; set; }

        public virtual Supplier SupplierDetail { get; set; }
    }

    [Table("IV_ItemManufacturer")]
    public class ItemManufacturer : TableStrutcture
    {
        [Key]
        public int ItemManufacturerId { get; set; }

        public int ItemId { get; set; }

        [ForeignKey("ManufacturerDetail")]
        public int ManufacturerId { get; set; }

        public string Notes { get; set; }

        public virtual Manufacturer ManufacturerDetail { get; set; }
    }
    [Table("IV_ItemProvision")]
    public class ItemProvision : TableStrutcture
    {
        [Key]
        public int ItemProvisionId { get; set; }

        public int ItemId { get; set; }

        public int IsCalculable { get; set; }

        public int MinimumStockLevel { get; set; }

        public int MaximumStockLevel { get; set; }

        public int MinimumOrderLevel { get; set; }

        public int MaximumOrderLevel { get; set; }

    }

}
