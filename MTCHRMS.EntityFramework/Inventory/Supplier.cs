using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.Inventory
{
    [Table("IV_Supplier")]
    public class Supplier : TableStrutcture
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Please enter Supplier Code !")]
        public string SupplierCode { get; set; }

        [Required(ErrorMessage = "Please enter Supplier Name !")]
        public string SupplierName { get; set; }

        public string Description { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }

        public string WebSite { get; set; }

        public int StatusId { get; set; }

        public ICollection<SupplierContactPerson> SupplierContactPersons { get; set; }

        public ICollection<SupplierContract> SupplierContracts { get; set; }

    }

    [Table("IV_SupplierContact")]
    public class SupplierContactPerson : TableStrutcture
    {

        [Key]
        public int ContactPersonId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public string ContactPerson { get; set; }

        public string Email { get; set; }

        public string MobileNo { get; set; }

        public string PhoneNo { get; set; }

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

        public string ContractNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Notes { get; set; }

        public int StatusId { get; set; }
    }

}
