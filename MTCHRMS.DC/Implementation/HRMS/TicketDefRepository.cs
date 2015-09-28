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
    public class TicketDefRepository: ITicketDefRepository
    {
        DbEntityContext _ctx;

        public TicketDefRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<TicketDef>> GetTickets()
        {
            //System.Threading.Thread.Sleep(100);
            return await Task.Run(() => _ctx.Tickets.Include(c=>c.ScheduleDetail).Include(d=>d.EligibilityDetail));
        }

        public TicketDef GetTicketDef(int id)
        {
            return _ctx.Tickets.Single(r=>r.TicketId == id);
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

        public bool AddTicketDef(TicketDef newTicket)
        {
            try
            {
                _ctx.Tickets.Add(newTicket);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateTicketDef(TicketDef updateTicket)
        {
            try
            {
                _ctx.Entry(updateTicket).State = EntityState.Modified;
                return true;

            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }
    }
}
