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
    public class StoreLocationController : ApiController
    {
        public IStoreLocationRepository _repo;

        public StoreLocationController(IStoreLocationRepository pRepository)
        {
            _repo = pRepository;
        }

        //[Authorize]
        public Task<IQueryable<StoreLocation>> Get()     
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side
            
            //System.Threading.Thread.Sleep(1000);
            var locations = _repo.GetLocations();

            return locations;
        }

        //[Authorize]
        public StoreLocation Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var location = _repo.GetLocation(id);

            if (location == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return location;
        }

        //[Authorize]
        public HttpResponseMessage Post([FromBody] StoreLocation newLocation)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newLocation.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                newLocation.CreatedOn = DateTime.Now;

                if (_repo.AddLocation(newLocation) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newLocation);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        //[Authorize]
        public HttpResponseMessage Put(int id, [FromBody] StoreLocation updateLocation)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateLocation.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateLocation.ModifiedOn = DateTime.Now;

                if (_repo.UpdateLocation(updateLocation) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateLocation);
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
