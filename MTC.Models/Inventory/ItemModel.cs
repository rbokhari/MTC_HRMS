using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTC.Models.Inventory
{
    public class ItemModel
    {
        public int Id { get; set; }
        
        [DisplayName("Item Code")]
        public String Code { get; set; }

        [DisplayName("Item Name")]
        public String Name { get; set; }

        [DisplayName("Part No")]
        public String PartNo { get; set; }

        [DisplayName("Serial No")]
        public String SerialNo { get; set; }

        public int TypeId { get; set; }
        public String Type { get; set; }

        public int CategoryId { get; set; }
        public String Category { get; set; }

        public int StoreId { get; set; }
        public String Store { get; set; }

        public decimal Stock { get; set; }

        public byte[] Picture { get; set; }
    }

    
}
