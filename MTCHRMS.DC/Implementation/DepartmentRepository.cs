using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;

namespace MTCHRMS.DC

{
    public class DepartmentRepository: IDepartmentsRepository
    {
        DbEntityContext _ctx;

        public DepartmentRepository(DbEntityContext ctx)
        {
            // here we can pass this as ref object, so that it can use everytime, like, DbEntityContext ctxContext
            // use then ninject to create constructor instance auto and reference it if available.

            //_ctx = new DbEntityContext();
            _ctx = ctx;
        }

        //public IQueryable<Department> GetDepartments()
        //{
        //    //System.Threading.Thread.Sleep(100);
        //    return _ctx.Departments;
        //}

        public async Task<IQueryable<Department>> GetDepartments()
        {
            return await Task.Run(() => _ctx.Departments);
        }

        public Department GetDepartment(int id)
        {
            return _ctx.Departments.Single(r=>r.Id == id);
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

        public bool AddDepartment(Department newDepartment)
        {
            try
            {
                //newDepartment.CreatedBy = 1;
                //newDepartment.CreatedOn = DateTime.UtcNow;
                _ctx.Departments.Add(newDepartment);

                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateDepartment(Department department)
        {
            try
            {
                //_ctx.Departments.Attach(_ctx.Departments.Single(r => r.Id == department.Id));
                _ctx.Entry(department).State = EntityState.Modified;
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
