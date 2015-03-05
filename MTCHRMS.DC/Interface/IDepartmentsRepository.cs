using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;

namespace MTCHRMS.DC
{
    public interface IDepartmentsRepository
    {
        Task<IQueryable<Department>> GetDepartments();

        //IQueryable<Department> GetDepartments();
        Department GetDepartment(int id);

        bool Save();

        bool AddDepartment(Department newDepartment);
        bool UpdateDepartment(Department newDepartment);
    }
}
