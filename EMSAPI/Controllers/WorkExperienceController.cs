using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class WorkExperienceController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (WorkExperienceEntities entities = new WorkExperienceEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.WORKEXPERIENCE.ToList());
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
                using (WorkExperienceEntities entities = new WorkExperienceEntities())
                {
                    WORKEXPERIENCE workexperience = entities.WORKEXPERIENCE.FirstOrDefault(e => e.WorkExperienceId == id);
                    if (workexperience != null)
                        return Request.CreateResponse(HttpStatusCode.OK, workexperience);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] WORKEXPERIENCE workexperience)
        {
            try
            {
                using (WorkExperienceEntities entities = new WorkExperienceEntities())
                {
                    entities.WORKEXPERIENCE.Add(workexperience);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, workexperience);
                    message.Headers.Location = new Uri(Request.RequestUri + workexperience.WorkExperienceId.ToString());
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
                using (WorkExperienceEntities entities = new WorkExperienceEntities())
                {
                    var entity = entities.WORKEXPERIENCE.FirstOrDefault(e => e.WorkExperienceId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "WorkExperience doesnt exist");
                    else
                    {
                        entities.WORKEXPERIENCE.Remove(entity);
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
        public HttpResponseMessage put(int id, WORKEXPERIENCE workexperience)
        {
            try
            {
                using (WorkExperienceEntities entities = new WorkExperienceEntities())
                {
                    var entity = entities.WORKEXPERIENCE.FirstOrDefault(e => e.WorkExperienceId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "WorkExperience doesnt exist");
                    else
                    {
                        entity.WorkExperienceId = workexperience.WorkExperienceId;
                        entity.EmployeeId = workexperience.EmployeeId;
                        entity.DocumentId = workexperience.DocumentId;
                        entity.CompanyName = workexperience.CompanyName;
                        entity.JobTitle = workexperience.JobTitle;
                        entity.StartDate = workexperience.StartDate;
                        entity.Enddate = workexperience.Enddate;
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
