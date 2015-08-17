using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.Inventory
{
    [Table("IV_Distribution")]
    public class Distribution : TableStrutcture
    {
        [Key]
        public int DistributionId { get; set; }

        [MaxLength(25)]
        public String ComputerCode { get; set; }

        public DateTime DistributionDate { get; set; }

        public int DepartmentId { get; set; }

        public int EmployeeId { get; set; }

        public int AuthorizedBy { get; set; }

        [MaxLength(250)]
        public String AuthorizedDesignation { get; set; }


        [MaxLength(500)]
        public String Notes { get; set; }

        public int StatusId { get; set; }

        public virtual ICollection<DistributionItem> DistributionItems { get; set; }
    }


    [Table("IV_DistributionItems")]
    public class DistributionItem : TableStrutcture
    {
        [Key]
        public int DistributionItemId { get; set; }


        public int DistributionId { get; set; }

        public int ItemId { get; set; }

        public int ItemStockSerialId { get; set; }

        public int StockInHand { get; set; }

        public int StatusId { get; set; }
    }

}
