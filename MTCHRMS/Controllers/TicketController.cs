using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Http;
//using System.Web.Mvc;
using System.Web.UI.WebControls;
using MTCHRMS.DC;
using MTCHRMS.EntityFramework;
using MTCHRMS.DC.Interface.HRMS;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.Controllers
{
    public class TicketController : ApiController
    {
        public ITicketDefRepository _repo;

        public TicketController(ITicketDefRepository pRepository)
        {
            _repo = pRepository;
        }

        [Authorize]
        public Task<IQueryable<TicketDef>> Get()     
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side
            var tickets = _repo.GetTickets();

            return tickets;
        }

        [Authorize]
        public TicketDef Get(int id)
        {
            var ticket = _repo.GetTicketDef(id);

            if (ticket == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return ticket;
        }

        [Authorize]
        public HttpResponseMessage Post([FromBody] TicketDef newTicket)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newTicket.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                newTicket.CreatedOn = DateTime.Now;

                if (_repo.AddTicketDef(newTicket) && _repo.Save())
                {
                    var response = Request.CreateResponse<TicketDef>(HttpStatusCode.Created, newTicket);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newTicket.TicketId })); // this will generate link
                    return response;
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody] TicketDef updateTicket)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateTicket.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateTicket.ModifiedOn = DateTime.Now;

                if (_repo.UpdateTicketDef(updateTicket) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateTicket);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
        
    }
}
