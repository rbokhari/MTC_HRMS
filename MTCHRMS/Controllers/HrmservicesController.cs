using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MTC.GlobalVariables;
using MTCHRMS.DC.Interface.HRMS;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.Controllers
{
    public class HrmservicesController : ApiController
    {
        public IServicesRepository repo;

        public HrmservicesController(IServicesRepository repo)
        {
            this.repo = repo;
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
                    newLeave.StatusId = (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_APPLIED;

                    if (repo.AddEmployeeApplyLeave(newLeave) && repo.Save())
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
                else if (newLeave.EmployeeLeaveId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newLeave.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newLeave.ModifiedOn = DateTime.Now;

                    if (repo.AddEmployeeApplyLeave(newLeave) && repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newLeave);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }

                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [HttpGet]
        [Route("api/hrmsservices/getServiceNotification")]
        public async Task<dynamic> GetEmployeeNotification()
        {
            var currentUser = Convert.ToInt32(Request.Headers.GetValues("userId").First());

            //var notifications = await Repo.GetEmployeeNotification(currentUser);

            return new
            {
                Notifications = await repo.GetEmployeeNotification(currentUser)
            };
        }

        [HttpGet]
        [Route("api/hrmsservices/getLeave/{id}")]
        public async Task<dynamic> GetEmployeeLeaveById(int id)
        {
            return new
            {
                Leave = await repo.GetEmployeeLeave(id)
            };
        }


        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
