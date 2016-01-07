using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MTCHRMS.EntityFramework.HRMS;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.EntityFramework.Training
{
    [Table("TR_CourseAssignment")]
    public class CourseAssignment : TableStrutcture
    {
        [Key]
        public int AssignmentId { get; set; }

        [ForeignKey("EmployeeDetail")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeDepartment")]
        public int DepartmentId { get; set; }

        [MaxLength(250)]
        public String Designation { get; set; }

        public int YearId { get; set; }

        [ForeignKey("ConsultantDepartment")]
        public int ConsultantDepartmentId { get; set; }

        [ForeignKey("ConsultantDetail")]
        public int ConsultanctEmployeeId { get; set; }

        [ForeignKey("AdvisorDepartment")]
        public int AdvisorDepartmentId { get; set; }

        [ForeignKey("AdvisorDetail")]
        public int AdvisorEmployeeId { get; set; }

        [ForeignKey("StatusDetail")]
        public int StatusId { get; set; }

        public virtual ICollection<CourseAssignmentDetail> CourseDetails { get; set; }

        public virtual EmployeeDef EmployeeDetail { get; set; }

        public virtual EmployeeDef ConsultantDetail { get; set; }

        public virtual EmployeeDef AdvisorDetail { get; set; }

        public virtual Department EmployeeDepartment { get; set; }

        public virtual Department ConsultantDepartment { get; set; }

        public virtual Department AdvisorDepartment { get; set; }

        public virtual ValidationDetail StatusDetail { get; set; }
    }


    [Table("TR_CourseAssignmentDetail")]
    public class CourseAssignmentDetail : TableStrutcture
    {
        [Key]
        public int AssignmentDetailId { get; set; }

        public int AssignmentId { get; set; }

        [ForeignKey("CategoryDetail")]
        public int CategoryId { get; set; }

        [ForeignKey("CourseDetail")]
        public int CourseId { get; set; }

        [MaxLength(250)]
        public String Duration { get; set; }

        [MaxLength(250)]
        public String TimePeriod { get; set; }

        [MaxLength(500)]
        public String Location { get; set; }

        [MaxLength(500)]
        public String Organizer { get; set; }


        public virtual ValidationDetail CategoryDetail { get; set; }

        public virtual CourseDef CourseDetail { get; set; }
    }


}
