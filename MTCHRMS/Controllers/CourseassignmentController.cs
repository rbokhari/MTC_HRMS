using MTCHRMS.DC.Interface.HRTR;
using MTCHRMS.EntityFramework.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTCHRMS.Controllers
{
    //[Authorize]
    [RoutePrefix("api/courseassignment")]
    public class CourseassignmentController : ApiController
    {
        private IAssignmentRepository repo;

        public CourseassignmentController(IAssignmentRepository repo)
        {
            this.repo = repo;
        }
        
        [HttpPost]
        public HttpResponseMessage Post([FromBody] CourseAssignment newAssignment)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newAssignment.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                newAssignment.CreatedOn = DateTime.Now;

                foreach (var item in newAssignment.CourseDetails)
                {
                    item.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    item.CreatedOn = DateTime.Now;
                }

                if (repo.AddAssignment(newAssignment) && repo.Save())
                {
                    var response = Request.CreateResponse<CourseAssignment>(HttpStatusCode.Created, newAssignment);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newAssignment.AssignmentId })); // this will generate link
                    return response;
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        public HttpResponseMessage Put([FromBody] CourseAssignment updateAssignment)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateAssignment.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateAssignment.ModifiedOn = DateTime.Now;

                foreach (var item in updateAssignment.CourseDetails)
                {
                    if (item.AssignmentDetailId == 0)
                    {
                        item.AssignmentId = updateAssignment.AssignmentId;
                        item.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                        item.CreatedOn = DateTime.Now;
                    }else
                    {
                        item.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                        item.ModifiedOn = DateTime.Now;
                    }
                }

                if (repo.UpdateAssignment(updateAssignment) && repo.Save())
                {
                    var response = Request.CreateResponse<CourseAssignment>(HttpStatusCode.Created, updateAssignment);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = updateAssignment.AssignmentId })); // this will generate link
                    return response;
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }


        [HttpGet]
        public async Task<dynamic> Get([FromUri] int departmentId, [FromUri] int yearId)
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            return new
            {
                status = HttpStatusCode.OK,
                fetch_date = DateTime.Now,
                assignments = await repo.GetAssignment(departmentId, yearId)
            };
        }

        public dynamic Get(int id)
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            return new
            {
                status = HttpStatusCode.OK,
                fetch_date = DateTime.Now,
                assignment = repo.GetAssignmentById(id)
            };
        }


        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
