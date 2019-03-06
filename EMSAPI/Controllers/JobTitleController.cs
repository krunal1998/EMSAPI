using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class JobTitleController : ApiController
    {
        // get job title List 
        public HttpResponseMessage GetJobTitle()
        {
            try
            {
                using (JobTitleEntities en = new JobTitleEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, en.JOBTITLE.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage GetJobTitle(int id)
        {
            try
            {
                using (JobTitleEntities en = new JobTitleEntities())
                {
                    JOBTITLE jobtitle= en.JOBTITLE.FirstOrDefault(e => e.JobTitleId == id);
                    if (jobtitle != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, jobtitle);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity found with id :" + id);
                    }
                }
            }
            
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //Add Job title
        public HttpResponseMessage Post([FromBody] JOBTITLE Jobtitle)
        {
            try
            {
                using (JobTitleEntities entity = new JobTitleEntities())
                {
                    entity.JOBTITLE.Add(Jobtitle);
                    entity.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, Jobtitle);
                    message.Headers.Location = new Uri(Request.RequestUri + Jobtitle.JobTitleId.ToString());
                    return message;
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Update JobTitle
        public HttpResponseMessage Put(int id, [FromBody] JOBTITLE jobtitle)
        {
            try
            {
                using (JobTitleEntities entity = new JobTitleEntities())
                {
                    var en = entity.JOBTITLE.Find(id);
                    if (en == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesn't exist");
                    }
                    else
                    {

                        en.JobTitleName = jobtitle.JobTitleName;
                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        //Delete job title
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (JobTitleEntities entity = new JobTitleEntities())
                {
                    var record = entity.JOBTITLE.FirstOrDefault(e => e.JobTitleId == id);
                    if (record == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesnt exist");
                    }
                    else
                    {
                        entity.JOBTITLE.Remove(record);
                        entity.SaveChanges();
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
