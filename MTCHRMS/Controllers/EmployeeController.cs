using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MTCHRMS.DC;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.HRMS;
using Newtonsoft.Json;

namespace MTCHRMS.Controllers
{
    public class EmployeeController : ApiController
    {
        public IEmployeesRepository _repo;

        public EmployeeController(IEmployeesRepository pRepository)
        {
            _repo = pRepository;
        }

        [Route("api/employee/")]
        [HttpGet]
        [Authorize]
        public IQueryable<EmployeeDef> Get()
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            var employees = _repo.GetEmployees();

            return employees;
        }

        [Authorize]
        public IQueryable<EmployeeDef> Get(int id)
        {
            //System.Threading.Thread.Sleep(4000);
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var employee = _repo.GetEmployeeDetail(id);

            if (employee == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return employee;
        }

        //[ActionName("GetEmployeeByUserName")]
        [HttpGet]
        [Route("api/employee/GetEmployeeByUserName")]
        [Authorize]
        public EmployeeDef EmployeeByUserName(string userName)
        {
            //System.Threading.Thread.Sleep(4000);
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var employee = _repo.GetEmployeeDetail(userName);

            if (employee == null)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return employee;
        }


        [ActionName("PostNewEmployee")]
        [HttpPost]
        [Route("api/employee/")]
        [Authorize]
        public HttpResponseMessage Post([FromBody] EmployeeDef newEmployee)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddEmployee(newEmployee) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newEmployee);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody] EmployeeDef updateEmployee)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                if (_repo.UpdateEmployee(updateEmployee) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateEmployee);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [HttpPost, Route("api/employee/upload")]
        [Authorize]
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }
            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            // Remove this line as well as GetFormData method if you're not 
            // sending any form data with your upload request
            int paramData = GetFormData<int>(result);

            HttpPostedFile _file = HttpContext.Current.Request.Files[0];

            var memstream = new MemoryStream();

            _file.InputStream.CopyTo(memstream);
            var employee = _repo.GetEmployee(paramData);
            employee.EmpPicture = memstream.ToArray();

            if (_repo.UpdateEmployeeImage(ref employee) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, employee);
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

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
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

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    

        //public async Task<HttpResponseMessage> UploadImage([FromBody] HttpPostedFileBase imgFile)
        //{
            
        //    if (ModelState.IsValid)
        //    {
        //        //if (!Request.Content.IsMimeMultipartContent())
        //        //    throw new Exception(); // divided by zero

        //        HttpPostedFile _file = HttpContext.Current.Request.Files[0];

        //        var memstream = new MemoryStream();

        //        _file.InputStream.CopyTo(memstream);
        //        //employee.EmpPicture = memstream.ToArray();

        //        //var provider = new MultipartMemoryStreamProvider();
        //        //await Request.Content.ReadAsMultipartAsync(provider);
        //        //var file = provider.Contents;

        //        //byte[] bytes = await file[0].ReadAsByteArrayAsync();
        //        //var ms = new MemoryStream(bytes);

        //        //if (_repo.UpdateEmployeeImage(employee, memstream) && _repo.Save())
        //        //{
        //        //    return Request.CreateResponse(HttpStatusCode.Created, employee);
        //        //    //return new HttpResponseMessage(HttpStatusCode.OK);
        //        //}
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
        //    }
        //    return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        //}


        [ActionName("PostEmployeePassport")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddEmployeePassport([FromBody] EmployeePassport newPassport)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddEmployeePassport(newPassport) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newPassport);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostEmployeeVisa")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddEmployeeVisa([FromBody] EmployeeVisa newVisa)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddEmployeeVisa(newVisa) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newVisa);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostEmployeePreviousEmployment")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddEmployeePreviousEmployment([FromBody] EmployeePreviousEmployment newPreviousEmployment)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddPreviousExperience(newPreviousEmployment) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newPreviousEmployment);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostEmployeeChild")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddEmployeeChild([FromBody] EmployeeChildren newChildren)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddEmployeeChild(newChildren) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newChildren);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostEmployeeMarital")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddEmployeeMarital([FromBody] EmployeeMarital newMarital)
        {
            if (ModelState.IsValid)
            {
                if (newMarital.Id == 0)
                {
                    if (_repo.AddEmployeeMaritals(newMarital) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newMarital);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }   
                }
                else if (newMarital.Id > 0)
                {
                    if (_repo.UpdateEmployeeMaritals(newMarital) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newMarital);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }                       
                }


                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostEmployeeKin")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddEmployeeKin([FromBody] EmployeeKin newKin)
        {
            if (ModelState.IsValid)
            {
                if (newKin.Id == 0)
                {
                    if (_repo.AddEmployeeKin(newKin) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newKin);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newKin.Id > 0)
                {
                    if (_repo.UpdateEmployeeKin(newKin) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newKin);
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
