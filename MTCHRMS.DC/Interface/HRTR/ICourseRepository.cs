using MTC.Models.Training;
using MTCHRMS.EntityFramework.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.DC.Interface.HRTR
{
    public interface ICourseRepository
    {
        Task<IQueryable<CourseModel>> GetCourses();

        CourseDef GetCourse(int id);

        bool Save();

        bool AddCourse(CourseDef newCourse);

        bool UpdateCourse(CourseDef updateCourse);
    }
}
