using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MTCHRMS.DC.Interface.HRMS;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.Controllers
{
    public class HrmservicesController : ApiController
    {
        public IServicesRepository Repo;

        public HrmservicesController(IServicesRepository repo)
        {
            Repo = repo;
        }

        //[ActionName("PostNewEmployee")]
        [HttpPost]
        [Route("api/hrmservices/postemployeeleave")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage PostEmployeeLeave([FromBody] EmployeeLeave newLeave)
        {
            if (ModelState.IsValid)
            {
                if (newLeave.EmployeeLeaveId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newLeave.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }

                    newLeave.CreatedOn = DateTime.Now;

                    if (Repo.AddEmployeeApplyLeave(newLeave) && Repo.Save())
                    {
                        //context.Clients.All.addNewEmployee(newEmployee.Id, newEmployee.CreatedBy);
                        return Request.CreateResponse(HttpStatusCode.Created, newLeave);
                    }
                    else
                    {
                        ModelState.AddModelError("Found", "Applied days are not available");
                        return Request.CreateResponse(HttpStatusCode.Found, GetErrorMessages());
                    }
                }
                //else if (newLeave.EmployeeLeaveId != 0)
                //{
                //    if (Request.Headers.Contains("userId"))
                //    {
                //        newLeave.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                //    }
                //    newLeave.ModifiedOn = DateTime.Now;

                //    if (Repo.UpdateEmployee(newLeave, GetRoleIdByUserId()) && _repo.Save())
                //    {
                //        return Request.CreateResponse(HttpStatusCode.Created, newEmployee);
                //        //return new HttpResponseMessage(HttpStatusCode.OK);
                //    }

                //}

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [HttpGet]
        [Route("api/hrmservices/getServiceNotification")]
        public async Task<dynamic> GetEmployeeNotification()
        {
            var currentUser = 5; // Convert.ToInt32(Request.Headers.GetValues("userId").First());

            //var notifications = await Repo.GetEmployeeNotification(currentUser);

            return new
            {
                Notifications = await Repo.GetEmployeeNotification(currentUser)
            };
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
