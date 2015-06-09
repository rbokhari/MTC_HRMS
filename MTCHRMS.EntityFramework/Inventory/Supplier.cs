using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.EntityFramework.Inventory
{
    [Table("IV_Supplier")]
    public class Supplier : TableStrutcture
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Please enter Supplier Code !"),MaxLength(25)]
        public string SupplierCode { get; set; }

        [Required(ErrorMessage = "Please enter Supplier Name !")]
        [MaxLength(500)]
        public string SupplierName { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [ForeignKey("CountryDetail")]
        public int CountryId { get; set; }

        public int CityId { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string WebSite { get; set; }

        public int StatusId { get; set; }

        public ICollection<SupplierContactPerson> SupplierContactPersons { get; set; }

        public ICollection<SupplierContract> SupplierContracts { get; set; }


        //public ICollection<ItemSupplier> ItemSuppliers { get; set; }

        public virtual ValidationDetail CountryDetail { get; set; }

    }

    [Table("IV_SupplierContact")]
    public class SupplierContactPerson : TableStrutcture
    {

        [Key]
        public int ContactPersonId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [MaxLength(500)]
        public string ContactPerson { get; set; }

        [MaxLength(255)]
        public string Position { get; set; }

        [MaxLength(500)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string MobileNo { get; set; }

        [MaxLength(50)]
        public string PhoneNo { get; set; }

        [MaxLength(50)]
        public string FaxNo { get; set; }

        public int StatusId { get; set; }
    }

    [Table("IV_SupplierContract")]
    public class SupplierContract : TableStrutcture
    {
        [Key]
        public int ContractId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [MaxLength(255)]
        public string ContractNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public int NotifyBeforeDays { get; set; }

        public int? StatusId { get; set; }
    }

}
