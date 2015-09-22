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
    public class LeaveController : ApiController
    {
        public ILeavesDefRepository _repo;

        public LeaveController(ILeavesDefRepository pRepository)
        {
            _repo = pRepository;
        }

        [Authorize]
        public Task<IQueryable<LeaveDef>> Get()     
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side
            var leaves = _repo.GetLeaves();

            return leaves;
        }

        [Authorize]
        public LeaveDef Get(int id)
        {
            var leave = _repo.GetLeave(id);

            if (leave == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return leave;
        }

        [Authorize]
        public HttpResponseMessage Post([FromBody] LeaveDef newLeave)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newLeave.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                newLeave.CreatedOn = DateTime.Now;

                if (_repo.AddLeave(newLeave) && _repo.Save())
                {
                    var response = Request.CreateResponse<LeaveDef>(HttpStatusCode.Created, newLeave);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newLeave.LeaveId })); // this will generate link
                    return response;
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody] LeaveDef updateLeave)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateLeave.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateLeave.ModifiedOn = DateTime.Now;

                if (_repo.UpdateLeave(updateLeave) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateLeave);
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
