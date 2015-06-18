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
    [Table("IV_Manufacturer")]
    public class Manufacturer : TableStrutcture
    {
        [Key]
        public int ManufacturerId { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage = "Please enter Name!")]
        public string ManufacturerName { get; set; }

        [ForeignKey("CountryDetail")]
        public int CountryId { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public int StatusId { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string WebSite { get; set; }

        public ICollection<ManufacturerContactPerson> ManufacturerContactPersons { get; set; }

        public ICollection<ManufacturerContract> ManufacturerContracts { get; set; }


        public virtual ValidationDetail CountryDetail { get; set; }
    }

    [Table("IV_ManufacturerContact")]
    public class ManufacturerContactPerson : TableStrutcture
    {

        [Key]
        public int ContactPersonId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

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

    [Table("IV_ManufacturerContract")]
    public class ManufacturerContract : TableStrutcture
    {
        [Key]
        public int ContractId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

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

