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
    public class SupplierController : ApiController
    {
        public ISuppliersRepository _repo;

        public SupplierController(ISuppliersRepository pRepository)
        {
            _repo = pRepository;
        }

        [Authorize]
        public Task<IQueryable<Supplier>> Get()     
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side
            
            //System.Threading.Thread.Sleep(1000);
            var suppliers = _repo.GetSuppliers();

            return suppliers;
        }

        [Authorize]
        public Supplier Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var supplier = _repo.GetSupplier(id);

            if (supplier == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return supplier;
        }

        [Authorize]
        public HttpResponseMessage Post([FromBody] Supplier newSupplier)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newSupplier.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                newSupplier.CreatedOn = DateTime.Now;
                newSupplier.StatusId = 1;

                if (_repo.AddSupplier(newSupplier) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newSupplier);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody] Supplier updateSupplier)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateSupplier.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateSupplier.ModifiedOn = DateTime.Now;

                if (_repo.UpdateSupplier(updateSupplier) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateSupplier);
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
