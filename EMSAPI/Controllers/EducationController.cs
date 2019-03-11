using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class EducationController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (EducationEntities entities = new EducationEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.EDUCATION.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage get(int id)
        {
            try
            {
                using (EducationEntities entities = new EducationEntities())
                {
                    EDUCATION education = entities.EDUCATION.FirstOrDefault(e => e.EducationId == id);
                    if (education != null)
                        return Request.CreateResponse(HttpStatusCode.OK, education);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] EDUCATION education)
        {
            try
            {
                using (EducationEntities entities = new EducationEntities())
                {
                    entities.EDUCATION.Add(education);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, education);
                    message.Headers.Location = new Uri(Request.RequestUri + education.EducationId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage delete(int id)
        {
            try
            {
                using (EducationEntities entities = new EducationEntities())
                {
                    var entity = entities.EDUCATION.FirstOrDefault(e => e.EducationId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Education doesnt exist");
                    else
                    {
                        entities.EDUCATION.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage put(int id, EDUCATION education)
        {
            try
            {
                using (EducationEntities entities = new EducationEntities())
                {
                    var entity = entities.EDUCATION.FirstOrDefault(e => e.EducationId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Education doesnt exist");
                    else
                    {
                        entity.EducationId = education.EducationId;
                        entity.EmployeeId = education.EmployeeId;
                        entity.DocumentId = education.DocumentId;
                        entity.Degree = education.Degree;
                        entity.Institution = education.Institution;
                        entity.StartYear = education.StartYear;
                        entity.EndYear = education.EndYear;
                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}