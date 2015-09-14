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
    [Table("IV_ItemAttachment")]
    public class ItemAttachment : TableStrutcture
    {
        [Key]
        public int AttachmentId { get; set; }

        public int ItemId { get; set; }

        //[ForeignKey("StoragePath")]
        public int StorageId { get; set; }

        public String FileName { get; set; }

        public String FileType { get; set; }

        public string FileIcon { get; set; }

        public string AttachmentGuid { get; set; }


        //public virtual StoragePath StoragePath { get; set; }
    }

}
