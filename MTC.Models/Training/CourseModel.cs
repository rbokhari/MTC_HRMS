using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTC.Models.Training
{
    public class CourseModel
    {
        public int Id { get; set; }

        public String Code { get; set; }

        public String Name { get; set; }

        public String Objectives { get; set; }

        public String CategoryNameAr { get; set; }

        public String CategoryNameEn { get; set; }

        public Int32 CategoryId { get; set; }

        public Int32 StatusId { get; set; }
    }
}
