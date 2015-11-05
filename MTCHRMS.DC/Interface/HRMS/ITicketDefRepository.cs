using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.DC.Interface.HRMS
{
    public interface ITicketDefRepository
    {
        Task<IQueryable<TicketDef>> GetTickets();

        TicketDef GetTicketDef(int id);

        bool Save();

        bool AddTicketDef(TicketDef newTicket);
        bool UpdateTicketDef(TicketDef updateTicket);
    }
}
