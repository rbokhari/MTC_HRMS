using MTCHRMS.EntityFramework.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.EntityFramework.Training
{
    [Table("TR_Course")]
    public class CourseDef : TableStrutcture
    {
        [Key]
        public int CourseId { get; set; }

        [MaxLength(50)]
        [Required]
        public String Code { get; set; }

        [MaxLength(500)]
        [Required]
        public String CourseName { get; set; }

        [MaxLength(500)]
        public String Objectives { get; set; }

        [MaxLength(500)]
        public String Description { get; set; }

        [ForeignKey("CategoryDetail")]
        [Required]
        public Int32 CategoryId { get; set; }

        public Int32 StatusId { get; set; }


        public virtual ValidationDetail CategoryDetail { get; set; }
    }
}
