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
using MTCHRMS.EntityFramework.Inventory;

namespace MTCHRMS.Controllers
{
    public class ManufacturerController : ApiController
    {
        public IManufacturersRepository _repo;

        public ManufacturerController(IManufacturersRepository pRepository)
        {
            _repo = pRepository;
        }

        [Authorize]
        public Task<IQueryable<Manufacturer>> Get()     
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side
            
            //System.Threading.Thread.Sleep(1000);
            var departments = _repo.GetManufacturers();

            return departments;
        }

        [Authorize]
        public Manufacturer Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var manufacturer = _repo.GetManufacturer(id);

            if (manufacturer == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return manufacturer;
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Manufacturer newManufacturer)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newManufacturer.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                newManufacturer.CreatedOn = DateTime.Now;

                if (_repo.AddManufacturer(newManufacturer) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newManufacturer);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody] Manufacturer updateManufacturer)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateManufacturer.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateManufacturer.ModifiedOn = DateTime.Now;

                if (_repo.UpdateManufacturer(updateManufacturer) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateManufacturer);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
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
