using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;
using MTCHRMS.DC;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.HRMS;
using MTCHRMS.Hubs;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Hubs;
using MTC.Models.HRMS;
using MTCHRMS.App_Start;
using Ninject;

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
        //[System.Web.Http.Authorize]
        public IQueryable<EmployeeModel> Get()
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);

            if (Request.Headers.Contains("userId"))
            {
                var employees = _repo.GetEmployees(GetRoleIdByUserId());
                return employees;
            }
            return null;
        }

        private Int32 GetRoleIdByUserId()
        {
            using (AccountController account = new AccountController(NinjectConfig.CreateKernel().Get<IAccountRepository>()))
            {
                var user = account.GetUserDetail(Convert.ToInt32(Request.Headers.GetValues("userId").First()));

                return user?.RoleId ?? 0;
            }
            
        }

        [Route("api/employee/GetEmployeePicture/{id}")]
        [HttpGet]
        //[Authorize]
        public async Task<EmployeeModel> GetEmployeePicture(int id)
        {
            var item = await _repo.GetEmployeePicture(id);

            if (item == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.inva);
            }
            return item;
        }

        [Route("api/employee/GetPassportExpiryList")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public async Task<List<EmployeeDef>> GetPassportExpiry()
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            return await _repo.GetEmployeesPassportExpiry();
        }

        [Route("api/employee/GetVisaExpiryList")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public async Task<List<EmployeeDef>> GetVisaExpiry()
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            return await _repo.GetEmployeesVisaExpiry();
        }

        [Route("api/employee/GetContractExpiryList")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public async Task<List<EmployeeDef>> GetContractExpiry()
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            return await _repo.GetEmployeesContractExpiry();
        }

        [Route("api/employee/GetProbationExpiryList")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public async Task<List<EmployeeDef>> GetProbationExpiry()
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            return await _repo.GetEmployeesProbationExpiry();
        }

        [Route("api/employee/GetAppraisalList")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public async Task<List<EmployeeDef>> GetAppraisalList()
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            return await _repo.GetEmployeesAppraisalList();
        }


        [Route("api/employee/GetEmployeeContactNoList")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public IQueryable GetEmployeeContactNoList()
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            return _repo.GetEmployeesContactNo();
        }

        [Route("api/employee/GetSingleEmployee")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public IQueryable<EmployeeModel> GetSingle(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            var employees = _repo.GetEmployees(GetRoleIdByUserId()).Where(c => c.Id == id);

            return employees;
        }

        [Route("api/employee/GetEmployeeSearch")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public async Task<List<EmployeeDef>> GetEmployeeSearch([FromUri]EmployeeDef employee)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            return await _repo.GetEmployeeSearch(employee, GetRoleIdByUserId());
        }

        [Route("api/employee/GetEmployeeByDepartmentId/{id}")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public async Task<List<EmployeeDef>> GetEmployeeByDepartmentId(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            //System.Threading.Thread.Sleep(1000);
            return await _repo.GetEmployeeByDepartmentId(id);
        }

        [System.Web.Http.Authorize]
        public IQueryable<EmployeeDef> Get(int id)
        {
            //System.Threading.Thread.Sleep(4000);
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var employee = _repo.GetEmployeeDetail(id, GetRoleIdByUserId());

            if (employee == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return employee;
        }

        [Route("api/employee/GetEmployeeLeaveTicketDetail/{id}")]
        //[System.Web.Http.Authorize]
        public EmployeeLeaveTicketModel GetLeaveTicketDetail(int id)
        {
            //System.Threading.Thread.Sleep(4000);
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var employee = _repo.GetEmployeeLeaveTicketDetail(id, GetRoleIdByUserId());

            if (employee == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return employee;
        }

        //[ActionName("GetEmployeeByUserName")]
        [HttpGet]
        [Route("api/employee/GetEmployeeByUserName")]
        [System.Web.Http.Authorize]
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


        //[ActionName("PostNewEmployee")]
        [HttpPost]
        [Route("api/employee/")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage Post([FromBody] EmployeeDef newEmployee)
        {
            if (ModelState.IsValid) 
            {
                if (newEmployee.Id == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newEmployee.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }

                    newEmployee.CreatedOn = DateTime.Now;

                    if (_repo.AddEmployee(newEmployee) && _repo.Save())
                    {
                        //var context = GlobalHost.ConnectionManager.GetHubContext<HRMSStaffHub>();

                        //context.Clients.All.addNewEmployee(newEmployee.Id, newEmployee.CreatedBy);

                        return Request.CreateResponse(HttpStatusCode.Created, newEmployee);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newEmployee.Id != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newEmployee.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newEmployee.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateEmployee(newEmployee, GetRoleIdByUserId()) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newEmployee);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }

                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        //[Route("api/employee/")]
        //[System.Web.Http.Authorize]
        //public HttpResponseMessage Put(int id, [FromBody] EmployeeDef updateEmployee)
        //{
        //    //return Request.CreateResponse(HttpStatusCode.OK);
        //    if (ModelState.IsValid)
        //    {
        //        if (Request.Headers.Contains("userId"))
        //        {
        //            updateEmployee.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
        //        }
        //        updateEmployee.ModifiedOn = DateTime.Now;

        //        if (_repo.UpdateEmployee(updateEmployee) && _repo.Save())
        //        {
        //            return Request.CreateResponse(HttpStatusCode.Created, updateEmployee);
        //            //return new HttpResponseMessage(HttpStatusCode.OK);
        //        }
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        //    }
        //    return null;
        //}

        [HttpPost, Route("api/employee/upload")]
        [System.Web.Http.Authorize]
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
                var employee = _repo.GetEmployee(paramData, GetRoleIdByUserId());


                //using (System.IO.Compression.GZipStream compressionStream = 
                //    new System.IO.Compression.GZipStream(_file.InputStream, System.IO.Compression.CompressionMode.Compress))
                //{
                //    //originalFileStream.CopyTo(compressionStream);

                //    employee.EmpPicture = compressionStream.toar;
                //}

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
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
        }

        public static byte[] compress(byte[] data)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                using (System.IO.Compression.GZipStream gzipStream = 
                    new System.IO.Compression.GZipStream(outStream, System.IO.Compression.CompressionMode.Compress))
                using (MemoryStream srcStream = new MemoryStream(data))
                    srcStream.CopyTo(gzipStream);
                return outStream.ToArray();
            }
        }

        public static byte[] decompress(byte[] compressed)
        {
            using (MemoryStream inStream = new MemoryStream(compressed))
            using (System.IO.Compression.GZipStream gzipStream = new System.IO.Compression.GZipStream(inStream, System.IO.Compression.CompressionMode.Decompress))
            using (MemoryStream outStream = new MemoryStream())
            {
                gzipStream.CopyTo(outStream);
                return outStream.ToArray();
            }
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
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeePassport([FromBody] EmployeePassport newPassport)
        {
            if (ModelState.IsValid)
            {
                if (newPassport.Id == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newPassport.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newPassport.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddEmployeePassport(newPassport) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newPassport);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newPassport.Id != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newPassport.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newPassport.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateEmployeePassport(newPassport, GetRoleIdByUserId()) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newPassport);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }   
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteEmployeePassport")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage DeleteEmployeePassport([FromBody] EmployeePassport deletePassport)
        {
            if (ModelState.IsValid)
            {

                if (_repo.DeleteEmployeePassport(deletePassport) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deletePassport);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }


        [ActionName("PostEmployeeVisa")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeeVisa([FromBody] EmployeeVisa newVisa)
        {
            if (ModelState.IsValid)
            {
                if (newVisa.Id == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newVisa.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newVisa.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddEmployeeVisa(newVisa) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newVisa);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newVisa.Id != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newVisa.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newVisa.ModifiedOn = DateTime.UtcNow;

                    if (_repo.UpdateEmployeeVisa(newVisa, GetRoleIdByUserId()) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newVisa);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteEmployeeVisa")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage DeleteEmployeeVisa([FromBody] EmployeeVisa newVisa)
        {
            if (ModelState.IsValid)
            {

                if (_repo.DeleteEmployeeVisa(newVisa) && _repo.Save())
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
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeePreviousEmployment([FromBody] EmployeePreviousEmployment newPreviousEmployment)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newPreviousEmployment.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }
                newPreviousEmployment.CreatedOn = DateTime.UtcNow;

                if (_repo.AddPreviousExperience(newPreviousEmployment) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newPreviousEmployment);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeletePreviousEmployement")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage DeletePreviousEmployement([FromBody] EmployeePreviousEmployment deleteEmployment)
        {
            if (ModelState.IsValid)
            {

                if (_repo.DeletePreviousExperience(deleteEmployment) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deleteEmployment);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostEmployeeChild")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeeChild([FromBody] EmployeeChildren newChildren)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newChildren.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }
                newChildren.CreatedOn = DateTime.UtcNow;

                if (_repo.AddEmployeeChild(newChildren) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newChildren);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteEmployeeChild")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage DeleteEmployeeChild([FromBody] EmployeeChildren deleteChild)
        {
            if (ModelState.IsValid)
            {

                if (_repo.DeleteEmployeeChild(deleteChild) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deleteChild);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostEmployeeMarital")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeeMarital([FromBody] EmployeeMarital newMarital)
        {
            if (ModelState.IsValid)
            {
                if (newMarital.Id == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newMarital.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newMarital.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddEmployeeMaritals(newMarital) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newMarital);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }   
                }
                else if (newMarital.Id > 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newMarital.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newMarital.ModifiedOn = DateTime.UtcNow;

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

        [ActionName("PostEmployeeQualification")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeeQualification([FromBody] EmployeeQualification newQualification)
        {
            if (ModelState.IsValid)
            {
                if (newQualification.Id == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newQualification.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newQualification.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddEmployeeQualification(newQualification) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newQualification);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newQualification.Id > 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newQualification.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newQualification.ModifiedOn = DateTime.UtcNow;

                    if (_repo.UpdateEmployeeQualification(newQualification) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newQualification);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }


                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("DeleteEmployeeQualification")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage DeleteEmployeeQualification([FromBody] EmployeeQualification deleteQualification)
        {
            if (ModelState.IsValid)
            {

                if (_repo.DeleteEmployeeQualification(deleteQualification) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, deleteQualification);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }


        [ActionName("PostEmployeeKin")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeeKin([FromBody] EmployeeKin newKin)
        {
            if (ModelState.IsValid)
            {
                if (newKin.Id == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newKin.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newKin.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddEmployeeKin(newKin) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newKin);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newKin.Id > 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newKin.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newKin.ModifiedOn = DateTime.UtcNow;

                    if (_repo.UpdateEmployeeKin(newKin) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newKin);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [Route("api/employee/GetEmployeeContract/{id}")]
        //[System.Web.Http.Authorize]
        public async Task<EmployeeContract> GetEmployeeContract(int id)
        {
            return await _repo.LoadEmployeeContract(id);

        }

        [ActionName("PostEmployeeContract")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeeContract([FromBody] EmployeeContract newContract)
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

                    if (_repo.AddEmployeeContract(newContract) && _repo.Save())
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

                    if (_repo.UpdateEmployeeContract(newContract) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newContract);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostEmployeeLeaveCategory")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeeLeaveCategory([FromBody] EmployeeLeaveCategory newLeave)
        {
            if (ModelState.IsValid)
            {
                if (newLeave.LeaveCategoryId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newLeave.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newLeave.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddEmployeeLeaveCategory(newLeave) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newLeave);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newLeave.LeaveCategoryId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newLeave.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newLeave.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateEmployeeLeaveCategory(newLeave) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newLeave);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [ActionName("PostEmployeeTicketCategory")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeeTicketCategory([FromBody] EmployeeTicketCategory newTicket)
        {
            if (ModelState.IsValid)
            {
                if (newTicket.TicketCategoryId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newTicket.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newTicket.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddEmployeeTicketCategory(newTicket) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newTicket);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newTicket.TicketCategoryId != 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newTicket.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newTicket.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateEmployeeTicketCategory(newTicket) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newTicket);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [HttpPost]
        [ActionName("PostEmployeeLeave")]
        [System.Web.Http.Authorize]
        public HttpResponseMessage AddEmployeeLeave([FromBody] EmployeeLeave newLeave)
        {
            if (ModelState.IsValid)
            {
                if (newLeave.EmployeeLeaveId == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newLeave.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newLeave.CreatedOn = DateTime.Now;
                    if (_repo.AddEmployeeLeave(newLeave) && _repo.Save())
                    {
                        //var context = GlobalHost.ConnectionManager.GetHubContext<HRMSStaffHub>();
                        //context.Clients.All.addNewEmployee(newEmployee.Id, newEmployee.CreatedBy);
                        return Request.CreateResponse(HttpStatusCode.Created, newLeave);
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
