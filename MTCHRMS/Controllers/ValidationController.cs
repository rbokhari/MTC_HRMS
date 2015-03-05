using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MTCHRMS.DC;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.Controllers
{
    [RoutePrefix("/api/validation")]
    public class ValidationController : ApiController
    {
        public IValidationRepository _repo;

        public ValidationController(IValidationRepository pRepository)
        {
            _repo = pRepository;
        }

        [ActionName("GetValidationDetailByValidationId")]
        [HttpGet]
        //[Authorize]
        public IQueryable<ValidationDetail> ValiationDetailsByValidationId(int id)
        {
            var validationDetails = _repo.GetValidationDetails(id);

            return validationDetails;
        }

        [ActionName("GetValiationById")]
        [HttpGet]
        //[Authorize]
        public Validation ValiationById(int id)
        {
            var validation = _repo.GetValidation(id);

            return validation;
        }

        [ActionName("GetValidationDetailById")]
        [HttpGet]
        //[Authorize]
        public ValidationDetail ValidationDetailById(int id)
        {
            var validationDetail = _repo.GetValidationDetail(id);

            if (validationDetail == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return validationDetail;
        }

        //[Authorize]
        public HttpResponseMessage Post([FromBody] ValidationDetail newValidationDetail)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddValidationDetail(newValidationDetail) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, newValidationDetail);
                    //return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.BadGateway, GetErrorMessages());
            }
            return null;
        }

        //[Authorize]
        public HttpResponseMessage Put(int id, [FromBody] ValidationDetail updateValidationDetail)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                if (_repo.UpdateValidationDetail(updateValidationDetail) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateValidationDetail);
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

