using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTC.Models.Inventory
{
    public class ItemModel
    {
        public int Id { get; set; }
        
        public String Code { get; set; }

        public String Name { get; set; }

        public String PartNo { get; set; }

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
