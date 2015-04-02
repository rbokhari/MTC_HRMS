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
        public string ManufacturerName { get; set; }

        [ForeignKey("CountryDetail")]
        public int CountryId { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public int StatusId { get; set; }

        public virtual ValidationDetail CountryDetail { get; set; }
    }

}

