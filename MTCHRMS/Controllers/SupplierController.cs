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
        public IQueryable<Supplier> Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var supplier = _repo.GetSupplierDetail(id);

            if (supplier == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return supplier;
        }

        //[HttpGet]
        //[Route("api/supplier/GetSupplierDetail")]
        //[System.Web.Http.Authorize]
        //public IQueryable<Supplier> SupplierDetail(int id)
        //{
        //    //System.Threading.Thread.Sleep(4000);
        //    //IDepartmentsRepository _repo = new DepartmentRepository();
        //    var supplier = _repo.GetSupplierDetail(id);

        //    if (supplier == null)
        //    {
        //        //return Request.CreateErrorResponse(HttpStatusCode.BadRequest)
        //    }
        //    return supplier;
        //}

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

        [ActionName("PostSupplierContact")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddSupplierContact([FromBody] SupplierContactPerson newContactPerson)
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

                    if (_repo.AddSupplierContact(newContactPerson) && _repo.Save())
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

                    if (_repo.UpdateSupplierContact(newContactPerson) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newContactPerson);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }

                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostSupplierContract")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddSupplierContract([FromBody] SupplierContract newContract)
        {
            if (ModelState.IsValid)
            {
                if (newContract.SupplierId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newContract.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newContract.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddSupplierContract(newContract) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newContract);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newContract.SupplierId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newContract.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newContract.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateSupplierContract(newContract) && _repo.Save())
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
