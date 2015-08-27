using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MTCHRMS.DC.Interface;
using MTCHRMS.EntityFramework.Inventory;
using MTC.Models.Inventory;

namespace MTCHRMS.Controllers
{
    public class DistributionController : ApiController
    {
        IDistributionRepository _repo;

        public DistributionController(IDistributionRepository repo)
        {
            _repo = repo;
        }

        [Route("api/distribution/{id}")]
        public async Task<HttpResponseMessage> GetDistribution(int id)
        {
            var dist = await _repo.GetDistribution(id);

            return Request.CreateResponse(HttpStatusCode.OK, dist);
        }

        [Route("api/distributionHierarchy/{id}")]
        public async Task<HttpResponseMessage> GetDistributionHierarchy(int id)
        {
            var dist = await _repo.GetDistributionHierarchy(id);

            return Request.CreateResponse(HttpStatusCode.OK, dist);
        }

        [Route("api/distribution/add/")]
        public async Task<HttpResponseMessage> SetDistribution(Distribution distribution)
        {
            var dist = new Distribution
            {
                DistributionDate = distribution.DistributionDate,
                DepartmentId = distribution.DepartmentId,
                EmployeeId = distribution.EmployeeId,
                AuthorizedBy = distribution.AuthorizedBy,
                AuthorizedDesignation = distribution.AuthorizedDesignation,
                Notes = distribution.Notes,
                CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First()),
                CreatedOn = DateTime.UtcNow
            };

            ICollection<DistributionItem> items = distribution.DistributionItems; // new List<DistributionItem>();

            if (_repo.AddDistribution(dist, items))
            {
                return Request.CreateResponse(HttpStatusCode.Created, dist);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
        }


        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
