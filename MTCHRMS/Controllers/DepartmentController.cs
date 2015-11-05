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

namespace MTCHRMS.Controllers
{
    public class DepartmentController : ApiController
    {
        public IDepartmentsRepository _repo ;

        public DepartmentController(IDepartmentsRepository pRepository)
        {
            _repo = pRepository;
        }

        [Authorize]
        public Task<IQueryable<Department>> Get()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side
            
            //System.Threading.Thread.Sleep(1000);
            var departments = _repo.GetDepartments();

            return departments;
        }

        [Authorize]
        public Department Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var department = _repo.GetDepartment(id);

            if (department == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return department;
        }

        [Authorize]
        [Route("/api/department")]
        public HttpResponseMessage Post([FromBody] Department newDepartment)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newDepartment.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                newDepartment.CreatedOn = DateTime.Now;

                if (_repo.AddDepartment(newDepartment) && _repo.Save())
                {
                    var response = Request.CreateResponse<Department>(HttpStatusCode.Created, newDepartment);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newDepartment.Id })); // this will generate link
                    return response;
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody] Department updateDepartment)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateDepartment.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateDepartment.ModifiedOn = DateTime.Now;

                if (_repo.UpdateDepartment(updateDepartment) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateDepartment);
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
