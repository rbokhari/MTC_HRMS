using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.General
{
    [Table("AC_AutoSetting")]
    public class AutoSetting
    {
        [Key]
        public int AutoSettingId { get; set; }

        public int ModuleId { get; set; }

        public int ScreenId { get; set; }

        public string CodePrefix { get; set; }

        public short CodeLength { get; set; }

        public byte IsAutoNumber { get; set; }

        public byte IsEditable { get; set; }

        public int StartValue { get; set; }

        public int CurrentValue { get; set; }


    }
}
