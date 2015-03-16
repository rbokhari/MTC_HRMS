using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MTCHRMS.DC;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Inventory;

namespace MTCHRMS.Controllers
{

    [RoutePrefix("/api/item")]
    public class ItemController : ApiController
    {
        public IItemsRepository _repo;

        public ItemController(IItemsRepository repo)
        {
            _repo = repo;
        }

        [Authorize]
        public Task<IQueryable<Item>> Get()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            var items = _repo.GetItems();

            return items;
        }

        [Authorize]
        public IQueryable<Item> Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var item = _repo.GetItem(id);

            if (item == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return item;
        }

        [Authorize]
        public HttpResponseMessage Post([FromBody] Item newItem)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newItem.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                newItem.CreatedOn = DateTime.Now;

                if (_repo.AddItem(newItem) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newItem);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody] Item updateItem)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateItem.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateItem.ModifiedOn = DateTime.Now;

                if (_repo.UpdateItem(updateItem) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateItem);
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
