using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class ComplainsController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (ComplainsEntities entities = new ComplainsEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.COMPLAINS.ToList());
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
                using (ComplainsEntities entities = new ComplainsEntities())
                {
                    COMPLAINS complains = entities.COMPLAINS.FirstOrDefault(e => e.ComplainId == id);
                    if (complains != null)
                        return Request.CreateResponse(HttpStatusCode.OK, complains);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] COMPLAINS complains)
        {
            try
            {
                using (ComplainsEntities entities = new ComplainsEntities())
                {
                    entities.COMPLAINS.Add(complains);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, complains);
                    message.Headers.Location = new Uri(Request.RequestUri + complains.ComplainId.ToString());
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
                using (ComplainsEntities entities = new ComplainsEntities())
                {
                    var entity = entities.COMPLAINS.FirstOrDefault(e => e.ComplainId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Complains doesnt exist");
                    else
                    {
                        entities.COMPLAINS.Remove(entity);
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
        public HttpResponseMessage put(int id, COMPLAINS complains)
        {
            try
            {
                using (ComplainsEntities entities = new ComplainsEntities())
                {
                    var entity = entities.COMPLAINS.FirstOrDefault(e => e.ComplainId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Complains doesnt exist");
                    else
                    {
                        entity.ComplainId = complains.ComplainId;
                        entity.EmployeeId = complains.EmployeeId;
                        entity.ComplainTypeId = complains.ComplainTypeId;
                        entity.ComplainDescription = complains.ComplainDescription;
                        entity.ComplainStatus = complains.ComplainStatus;
                        entity.ComplainDate = complains.ComplainDate;
                        entity.FeedbackRating = complains.FeedbackRating;
                        entity.feedbackDescription = complains.feedbackDescription;
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
