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
        public IQueryable<Manufacturer> Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var manufacturer = _repo.GetManufacturerDetail(id);

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

        [ActionName("PostManufacturerContact")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddManufacturerContact([FromBody] ManufacturerContactPerson newContactPerson)
        {
            if (ModelState.IsValid)
            {
                if (newContactPerson.ContactPersonId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newContactPerson.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newContactPerson.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddManufacturerContact(newContactPerson) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newContactPerson);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newContactPerson.ContactPersonId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newContactPerson.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newContactPerson.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateManufacturerContact(newContactPerson) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newContactPerson);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }

                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostManufacturerContract")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddManufacturerContract([FromBody] ManufacturerContract newContract)
        {
            if (ModelState.IsValid)
            {
                if (newContract.ContractId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newContract.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newContract.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddManufacturerContract(newContract) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newContract);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newContract.ContractId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newContract.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newContract.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateManufacturerContract(newContract) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newContract);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }

                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
        
    }
}
