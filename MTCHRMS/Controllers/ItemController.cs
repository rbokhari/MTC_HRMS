﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MTCHRMS.DC;
using MTCHRMS.EntityFramework.Inventory;
using System.Web;
using System.IO;

namespace MTCHRMS.Controllers
{

    //[RoutePrefix("/api/item")]
    public class ItemController : ApiController
    {
        public IItemsRepository _repo;

        public ItemController(IItemsRepository repo)
        {
            _repo = repo;
        }

        [Route("api/item/")]
        [HttpGet]
        [Authorize]
        public Task<IQueryable<Item>> Get()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            //System.Threading.Thread.Sleep(1000);
            var items = _repo.GetItems();

            return items;
        }

        [Route("api/item/GetSingleItem")]
        [HttpGet]
        [Authorize]
        public IQueryable<Item> Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var item = _repo.GetItemDetail(id);

            if (item == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return item;
        }

        [Route("api/item/ItemDepartments")]
        [HttpGet]
        [Authorize]
        public async Task<List<ItemDepartment>> GetItemDepartments()
        {
            var departments = _repo.GetItemDepartments();
            return await departments.ToListAsync();
        }

        [Route("api/item/ItemSuppliers")]
        [HttpGet]
        [Authorize]
        public async Task<List<ItemSupplier>> GetItemSuppliers()
        {
            var suppliers = _repo.GetItemSuppliers();
            return await suppliers.ToListAsync();
        }

        [Route("api/item/ItemYears")]
        [HttpGet]
        [Authorize]
        public async Task<List<ItemYear>> GetItemYears()
        {
            var years = _repo.GetItemYears();
            return await years.ToListAsync();
        }

        [Route("api/item/ItemManufacturers")]
        [HttpGet]
        [Authorize]
        public async Task<List<ItemManufacturer>> GetItemManufacturers()
        {
            var manufacturers = _repo.GetItemManufactuers();
            return await manufacturers.ToListAsync();
        }


        [Route("api/item/GetItemSearch")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public async Task<List<Item>> GetItemSearch([FromUri]Item item)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            return await _repo.GetItemSearch(item);
        }


        [Route("api/item/")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Post([FromBody] Item newItem)
        {
            if (ModelState.IsValid)
            {
                if (newItem.ItemId == 0)
                {


                    if (Request.Headers.Contains("userId"))
                    {
                        newItem.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }

                    newItem.CreatedOn = DateTime.Now;

                    if (_repo.CheckItemDuplicate(newItem.ItemId, newItem.ItemCode))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItem);
                    }

                    if (_repo.AddItem(newItem) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newItem);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItem.ItemId!=0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItem.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItem.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateItem(newItem) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newItem);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Route("api/item/upload")]
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> Upload()
        {
            try
            {

                if (!Request.Content.IsMimeMultipartContent())
                {
                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }
                HttpPostedFile _file = HttpContext.Current.Request.Files[0];

                var provider = GetMultipartProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                // Remove this line as well as GetFormData method if you're not 
                // sending any form data with your upload request
                int paramData = GetFormData<int>(result);

                var memstream = new MemoryStream();

                _file.InputStream.CopyTo(memstream);
                var item = _repo.GetItem(paramData);
                item.ItemPicture = memstream.ToArray();

                if (_repo.UpdateItemImage(ref item) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, item);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                // so this is how you can get the original file name
                //var originalFileName = GetDeserializedFileName(result.FileData.First());

                // uploadedFileInfo object will give you some additional stuff like file length,
                // creation time, directory name, a few filesystem methods etc..
                //var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);


                // Through the request response you can return an object to the Angular controller
                // You will be able to access this in the .success callback through its data attribute
                // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
                //var returnData = "ReturnTest";
                //return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
        }

        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private int GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                {
                    //return JsonConvert.DeserializeObject<T>(unescapedFormData);
                    return Convert.ToInt32(unescapedFormData); // JsonConvert.DeserializeObject(unescapedFormData);
                }
            }

            return 0;
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


        [ActionName("PostItemDepartment")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddItemDepartment([FromBody] ItemDepartment newItemDepartment)
        {
            if (ModelState.IsValid)
            {
                if (newItemDepartment.ItemDepartmentId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemDepartment.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemDepartment.CreatedOn = DateTime.UtcNow;

                    if (_repo.CheckItemDepartmentDuplicate(newItemDepartment.ItemId, 0, newItemDepartment.DepartmentId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemDepartment);
                    }
                    if (_repo.AddItemDepartment(newItemDepartment) && _repo.Save())
                    {
                        newItemDepartment = _repo.GetItemDepartment(newItemDepartment.ItemDepartmentId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemDepartment);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItemDepartment.ItemDepartmentId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemDepartment.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemDepartment.ModifiedOn = DateTime.Now;

                    if (_repo.CheckItemDepartmentDuplicate(newItemDepartment.ItemId, newItemDepartment.ItemDepartmentId, newItemDepartment.DepartmentId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemDepartment);
                    }

                    if (_repo.UpdateItemDepartment(newItemDepartment) && _repo.Save())
                    {
                        newItemDepartment = _repo.GetItemDepartment(newItemDepartment.ItemDepartmentId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemDepartment);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteItemDepartment")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteItemDepartment([FromBody] ItemDepartment deleteDepartment)
        {
            if (ModelState.IsValid)
            {

                if (_repo.DeleteItemDepartment(deleteDepartment) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deleteDepartment);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }


        [ActionName("PostItemYear")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddItemYear([FromBody] ItemYear newItemYear)
        {
            if (ModelState.IsValid)
            {
                if (newItemYear.ItemYearId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemYear.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemYear.CreatedOn = DateTime.UtcNow;

                    if (_repo.CheckItemYearDuplicate(newItemYear.ItemId, 0, newItemYear.YearId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemYear);
                    }


                    if (_repo.AddItemYear(newItemYear) && _repo.Save())
                    {
                        newItemYear = _repo.GetItemYear(newItemYear.ItemYearId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemYear);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItemYear.ItemYearId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemYear.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemYear.ModifiedOn = DateTime.Now;

                    if (_repo.CheckItemYearDuplicate(newItemYear.ItemId, newItemYear.ItemYearId, newItemYear.YearId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemYear);
                    }

                    if (_repo.UpdateItemYear(newItemYear) && _repo.Save())
                    {
                        newItemYear = _repo.GetItemYear(newItemYear.ItemYearId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemYear);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteItemYear")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteItemYear([FromBody] ItemYear deleteYear)
        {
            if (ModelState.IsValid)
            {

                if (_repo.DeleteItemYear(deleteYear) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deleteYear);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostItemSupplier")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddItemSupplier([FromBody] ItemSupplier newItemSupplier)
        {
            if (ModelState.IsValid)
            {
                if (newItemSupplier.ItemSupplierId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemSupplier.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemSupplier.CreatedOn = DateTime.UtcNow;

                    if (_repo.CheckItemSupplierDuplicate(newItemSupplier.ItemId, 0, newItemSupplier.SupplierId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemSupplier);
                    }


                    if (_repo.AddItemSupplier(newItemSupplier) && _repo.Save())
                    {
                        newItemSupplier = _repo.GetItemSupplier(newItemSupplier.ItemSupplierId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemSupplier);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItemSupplier.ItemSupplierId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemSupplier.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemSupplier.ModifiedOn = DateTime.Now;

                    if (_repo.CheckItemSupplierDuplicate(newItemSupplier.ItemId, newItemSupplier.ItemSupplierId, newItemSupplier.SupplierId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemSupplier);
                    }

                    if (_repo.UpdateItemSupplier(newItemSupplier) && _repo.Save())
                    {
                        newItemSupplier = _repo.GetItemSupplier(newItemSupplier.ItemSupplierId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemSupplier);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteItemSupplier")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteItemSupplier([FromBody] ItemSupplier deleteSupplier)
        {
            if (ModelState.IsValid)
            {
                if (_repo.DeleteItemSupplier(deleteSupplier) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deleteSupplier);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostItemManufacturer")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddItemManufacturer([FromBody] ItemManufacturer newItemManufacturer)
        {
            if (ModelState.IsValid)
            {
                if (newItemManufacturer.ItemManufacturerId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemManufacturer.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemManufacturer.CreatedOn = DateTime.UtcNow;

                    if (_repo.CheckItemManufacturerDuplicate(newItemManufacturer.ItemId, 0, newItemManufacturer.ManufacturerId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemManufacturer);
                    }


                    if (_repo.AddItemManufacturer(newItemManufacturer) && _repo.Save())
                    {
                        newItemManufacturer = _repo.GetItemManufactuer(newItemManufacturer.ItemManufacturerId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemManufacturer);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newItemManufacturer.ItemManufacturerId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newItemManufacturer.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newItemManufacturer.ModifiedOn = DateTime.Now;

                    if (_repo.CheckItemManufacturerDuplicate(newItemManufacturer.ItemId, newItemManufacturer.ItemManufacturerId, newItemManufacturer.ManufacturerId))
                    {
                        return Request.CreateResponse(HttpStatusCode.Found, newItemManufacturer);
                    }

                    if (_repo.UpdateItemManufacturer(newItemManufacturer) && _repo.Save())
                    {
                        newItemManufacturer = _repo.GetItemManufactuer(newItemManufacturer.ItemManufacturerId).FirstOrDefault();
                        return Request.CreateResponse(HttpStatusCode.Created, newItemManufacturer);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteItemManufacturer")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage DeleteItemManufacturer([FromBody] ItemManufacturer deleteManufacturer)
        {
            if (ModelState.IsValid)
            {
                if (_repo.DeleteItemManufacturer(deleteManufacturer) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, deleteManufacturer);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
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
