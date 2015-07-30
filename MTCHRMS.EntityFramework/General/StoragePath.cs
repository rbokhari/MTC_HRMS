using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.General
{
    [Table("CM_StoragePath")]
    public class StoragePath
    {
        [Key]
        public int StorageId { get; set; }

        public int ModuleId { get; set; }

        [MaxLength(2000)]
        public String FullPath { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int StatusId { get; set; }
    }
}
