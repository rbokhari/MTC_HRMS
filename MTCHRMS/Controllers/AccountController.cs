using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication.ExtendedProtection;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MTCHRMS.EntityFramework.General;
using MTCHRMS.DC;
using MTCHRMS.EntityFramework.Security;

namespace MTCHRMS.Controllers
{
    public class AccountController : ApiController
    {
        private IAccountRepository _repo;

        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public HttpResponseMessage Post(string userName, string password)
        {
            bool isValid = false;
            //using (var pc = new PrincipalContext(ContextType.Domain, "mtc.edu.om"))
            //{
            //    isValid = pc.ValidateCredentials(userName, password);
            //}
            //isValid = true;
            
            return isValid ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [ActionName("GetUserDetail")]
        [Route("api/account/GetUserDetail/")]
        [HttpGet]
        
        public AC_User GetUserDetail()
        {
            var user = _repo.GetUser(1);
            return user;
        }

        [ActionName("GetRoleDetail")]
        //[Route("api/account")]
        [HttpGet]
        public AC_Role GetRoleDetail(int id)
        {
            var role = _repo.GetRole(id);
            return role;
        }

        [ActionName("GetModuleDetail")]
        [Route("api/account/GetModuleDetail")]
        [HttpGet]
        public AC_Module GetModuleDetail()
        {
            var module = _repo.GetModule(1);
            return module;
        }

    }

}
