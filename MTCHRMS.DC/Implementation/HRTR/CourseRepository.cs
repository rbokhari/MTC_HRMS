using MTC.Models.Training;
using MTCHRMS.DC.Interface.HRTR;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Training;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCHRMS.DC.Implementation.HRTR
{
    public class CourseRepository : ICourseRepository
    {

        DbEntityContext _ctx;

        public CourseRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<CourseModel>> GetCourses()
        {
            return await Task.Run(() => 
                _ctx.Courses.Select(x => new CourseModel
                {
                    Id = x.CourseId,
                    Code = x.Code,
                    Name = x.CourseName,
                    Objectives = x.Objectives,
                    StatusId = x.StatusId,
                    CategoryId  = x.CategoryId,
                    CategoryNameAr = x.CategoryDetail.NameAr,
                    CategoryNameEn = x.CategoryDetail.NameEn

                }));
        }

        public CourseDef GetCourse(int id)
        {
            return _ctx.Courses.Single(r => r.CourseId == id);
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception)
            {
                // TODO log this error
                return false;
            }
        }

        public bool AddCourse(CourseDef newCourse)
        {
            try
            {
                _ctx.Courses.Add(newCourse);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateCourse(CourseDef updateCourse)
        {
            try
            {
                _ctx.Entry(updateCourse).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }
    }
}

