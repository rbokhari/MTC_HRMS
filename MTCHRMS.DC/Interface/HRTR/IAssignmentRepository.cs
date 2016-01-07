using MTC.Models.HRMS;
using MTCHRMS.EntityFramework.HRMS;
using MTCHRMS.EntityFramework.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.DC.Interface.HRTR
{
    public interface IAssignmentRepository
    {
        bool Save();

        bool AddAssignment(CourseAssignment newAssignment);

        bool UpdateAssignment(CourseAssignment newAssignment);

        bool DeleteAssignmentCourse(CourseAssignmentDetail course);

        Task<IQueryable<EmployeeModel>> GetAssignment(int departmentId, int yearId);

        CourseAssignment GetAssignmentById(int assignmentId);
    }
}
