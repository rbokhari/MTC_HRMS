using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTC.Models.Inventory
{
    public class ItemDistributionSerialModel
    {
        public int ItemId { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public int? StockSerialId { get; set; }

        public int? StockAddId { get; set; }

        public string StockSerialNo { get; set; }

        public string StockComputerCode { get; set; }

        public string StockReceiptNo { get; set; }

        public string Status { get; set; }

        public DateTime DeliveryDate { get; set; }

        public DateTime? WarrantyStateDate { get; set; }

        public DateTime? WarrantyEndDate { get; set; }

        public byte[] ItemPicture { get; set; }

        public virtual IQueryable<ItemDistributionSerialDetailModel> DistributionDetails { get; set; }
    }

    public class ItemDistributionSerialDetailModel
    {
        public int SerialDetailId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public byte[] EmployeePicture { get; set; }

        public DateTime AssignedDate { get; set; }

        public int? LocationId { get; set; }

        public string LocationName { get; set; }

        public string ContentClass { get; set; }  // for styling
    }

}
