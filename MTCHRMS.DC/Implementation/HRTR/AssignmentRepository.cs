using MTCHRMS.DC.Interface.HRTR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.Training;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.HRMS;
using MTC.Models.HRMS;
using System.Data.Entity;

namespace MTCHRMS.DC.Implementation.HRTR
{
    public class AssignmentRepository : IAssignmentRepository
    {
        DbEntityContext context;
        public AssignmentRepository(DbEntityContext ctx)
        {
            context = ctx;
        }

       

        public async Task<IQueryable<EmployeeModel>> GetAssignment(int departmentId, int yearId)
        {
            //var query = context.EmployeeDefs
            //    .Join(context.CourseAssignments,
            //        c => c.Id,
            //        cm => cm.EmployeeId,
            //        (c, cm) => new { Employee = c, Assign = cm })
            //        .Select(x => x.Employee);

            //var employees = context.EmployeeDefs.Where(x => x.PostedTo == departmentId);
            //var assignments = context.CourseAssignments.Where(x => x.YearId == yearId);

            var query = from emp in context.EmployeeDefs.Where(x => x.PostedTo == departmentId)
                        join d in context.CourseAssignments.Where(x => x.YearId == yearId)
                        on emp.Id equals d.EmployeeId into output
                        from j in output.DefaultIfEmpty()
                        select new EmployeeModel
                        {
                            Id = emp.Id,
                            Code = emp.EmployeeCode,
                            NameEn = emp.EmployeeName,
                            NameAr = emp.EmployeeNameAr,
                            Rank = emp.Rank,
                            DepartmentId = emp.PostedTo,
                            DepartmentName = emp.DepartmentId.DepartmentName,
                            DepartmentNameAr = emp.DepartmentId.DepartmentNameAr,
                            DesignationAr = emp.DesignationAr,
                            DesignationEn = emp.Designation,

                            CourseAssignmentId = j.AssignmentId,
                            CourseCount = j.CourseDetails.Count
                        };


                    return await Task.Run(() =>
                        //context.EmployeeDefs.Join.CourseAssignments.Include("CourseDetails").Include("employeeDetail")
                        query
                    );

        }

        //public async Task<IQueryable<CourseAssignment>> GetAssignmentById(int assignmentId)
        //{
        //    return await Task.Run(() =>
        //        context.CourseAssignments.Where(c => c.AssignmentId == assignmentId)
        //    );
        //}

        public CourseAssignment GetAssignmentById(int assignmentId)
        {
            var assignments = context
                .CourseAssignments
                .Include("CourseDetails").Include("CourseDetails.CategoryDetail").Include("CourseDetails.CourseDetail")
                .Include("EmployeeDetail").Include("EmployeeDetail.departmentId")
                .Include("ConsultantDetail").Include("ConsultantDetail.departmentId")
                .Include("AdvisorDetail").Include("AdvisorDetail.departmentId")
                .SingleOrDefault(c => c.AssignmentId == assignmentId);

            return assignments;
        }

        public bool Save()
        {
            try
            {
                return context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                // TODO log this error
                return false;
            }
        }

        public bool AddAssignment(CourseAssignment newAssignment)
        {
            try
            {
                context.CourseAssignments.Add(newAssignment);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateAssignment(CourseAssignment updateAssignment)
        {
            try
            {
                var assignmentFetch = GetAssignmentById(updateAssignment.AssignmentId);
                var attachedAssignment = context.Entry(assignmentFetch);
                attachedAssignment.CurrentValues.SetValues(updateAssignment);

                foreach (var item in updateAssignment.CourseDetails)
                {
                    if (item.AssignmentDetailId == 0)
                    {
                        context.CourseAssignmentDetails.Add(item);
                    }
                }

                //context.Entry(newAssignment).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error
                return false;
            }
        }

        public bool DeleteAssignmentCourse(CourseAssignmentDetail course)
        {
            throw new NotImplementedException();
        }
    }
}
