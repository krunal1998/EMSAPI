using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;
namespace EMSAPI.Controllers
{
    public class DeductionsController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (DeductionsEntities entities = new DeductionsEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.DEDUCTIONS.ToList());
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
                using (DeductionsEntities entities = new DeductionsEntities())
                {
                    DEDUCTIONS deductions = entities.DEDUCTIONS.FirstOrDefault(e => e.DeductionId == id);
                    if (deductions != null)
                        return Request.CreateResponse(HttpStatusCode.OK, deductions);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] DEDUCTIONS deductions)
        {
            try
            {
                using (DeductionsEntities entities = new DeductionsEntities())
                {
                    entities.DEDUCTIONS.Add(deductions);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, deductions);
                    message.Headers.Location = new Uri(Request.RequestUri + deductions.DeductionId.ToString());
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
                using (DeductionsEntities entities = new DeductionsEntities())
                {
                    var entity = entities.DEDUCTIONS.FirstOrDefault(e => e.DeductionId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Deductions doesnt exist");
                    else
                    {
                        entities.DEDUCTIONS.Remove(entity);
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
        public HttpResponseMessage put(int id, DEDUCTIONS deductions)
        {
            try
            {
                using (DeductionsEntities entities = new DeductionsEntities())
                {
                    var entity = entities.DEDUCTIONS.FirstOrDefault(e => e.DeductionId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Deductions doesnt exist");
                    else
                    {
                        entity.DeductionId = deductions.DeductionId;
                        entity.DeductionName = deductions.DeductionName;
                      
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
