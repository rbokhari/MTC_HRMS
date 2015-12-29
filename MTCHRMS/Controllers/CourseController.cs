using MTC.Models.Training;
using MTCHRMS.DC.Interface.HRTR;
using MTCHRMS.EntityFramework.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MTCHRMS.Controllers
{
    //[Authorize]
    [RoutePrefix("api/course")]
    public class CourseController : ApiController
    {
        public ICourseRepository _repo;

        public CourseController(ICourseRepository pRepository)
        {
            _repo = pRepository;
        }

        [HttpGet]
        public Task<IQueryable<CourseModel>> Get()
        {
            //System.Threading.Thread.Sleep(1000);
            var courses = _repo.GetCourses();

            return courses;
        }

        public CourseDef Get(int id)
        {
            var course = _repo.GetCourse(id);
            if (course == null)
            {
                //return Request.CreateErrorResponse(new Httprequest, HttpStatusCode.BadRequest);
            }
            return course;
        }

        [Authorize]
        public HttpResponseMessage Post([FromBody] CourseDef newCourse)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    newCourse.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                newCourse.CreatedOn = DateTime.Now;

                if (_repo.AddCourse(newCourse) && _repo.Save())
                {
                    var response = Request.CreateResponse<CourseDef>(HttpStatusCode.Created, newCourse);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newCourse.CourseId })); // this will generate link
                    return response;
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody] CourseDef updateCourse)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.Contains("userId"))
                {
                    updateCourse.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                }

                updateCourse.ModifiedOn = DateTime.Now;

                if (_repo.UpdateCourse(updateCourse) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateCourse);
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
