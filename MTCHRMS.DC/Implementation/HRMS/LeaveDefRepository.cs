using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.DC.Interface.HRMS;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.DC.Implementation.HRMS

{
    public class LeaveDefRepository: ILeavesDefRepository
    {
        DbEntityContext _ctx;

        public LeaveDefRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<LeaveDef>> GetLeaves()
        {
            //System.Threading.Thread.Sleep(100);
            return await Task.Run(() => 
                    _ctx.Leaves
                    .Include(c=>c.TypeDetail)
                    .Include(d=>d.ScheduleDetail));
        }

        public LeaveDef GetLeave(int id)
        {
            return _ctx.Leaves.Single(r=>r.LeaveId == id);
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

        public bool AddLeave(LeaveDef newLeave)
        {
            try
            {
                _ctx.Leaves.Add(newLeave);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateLeave(LeaveDef updateLeave)
        {
            try
            {
                //_ctx.Departments.Attach(_ctx.Departments.Single(r => r.Id == department.Id));
                _ctx.Entry(updateLeave).State = EntityState.Modified;
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
