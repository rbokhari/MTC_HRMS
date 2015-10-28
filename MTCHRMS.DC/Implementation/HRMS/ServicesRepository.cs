using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.DC.Interface.HRMS;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.DC.Implementation.HRMS
{
    public class ServicesRepository : IServicesRepository
    {
        DbEntityContext _ctx;

        public ServicesRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public bool AddEmployeeApplyLeave(EmployeeLeave newLeave)
        {
            try
            {
                var empLeaves = _ctx.EmployeeLeaveYears.FirstOrDefault(c => c.LeaveYearId == newLeave.LeaveYearId);
                if (empLeaves != null)
                {
                    if ((empLeaves.LeaveDays + empLeaves.TransferDays - empLeaves.DeductDays - empLeaves.AvailedDays) >= newLeave.LeaveDays)
                    {
                        _ctx.EmployeeLeaves.Add(newLeave);
                        empLeaves.AvailedDays += newLeave.LeaveDays;
                        _ctx.Entry(empLeaves).State = EntityState.Modified;
                        return true;
                    }
                    else
                    {
                        
                    }
                }
                return false;                
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
        }
    }
}
