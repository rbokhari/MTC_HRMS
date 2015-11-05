using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.DC.Interface.HRMS
{
    public interface ILeavesDefRepository
    {
        Task<IQueryable<LeaveDef>> GetLeaves();

        //IQueryable<Department> GetDepartments();
        LeaveDef GetLeave(int id);

        bool Save();

        bool AddLeave(LeaveDef newLeave);
        bool UpdateLeave(LeaveDef updateLeave);
    }
}
