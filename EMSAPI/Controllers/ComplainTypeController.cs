using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class ComplainTypeController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (ComplainTypeEntities entities = new ComplainTypeEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.COMPLAINTYPES.ToList());
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
                using (ComplainTypeEntities entities = new ComplainTypeEntities())
                {
                    COMPLAINTYPES complaintype = entities.COMPLAINTYPES.FirstOrDefault(e => e.ComplaintypeId == id);
                    if (complaintype != null)
                        return Request.CreateResponse(HttpStatusCode.OK, complaintype);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] COMPLAINTYPES complaintype)
        {
            try
            {
                using (ComplainTypeEntities entities = new ComplainTypeEntities())
                {
                    entities.COMPLAINTYPES.Add(complaintype);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, complaintype);
                    message.Headers.Location = new Uri(Request.RequestUri + complaintype.ComplaintypeId.ToString());
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
                using (ComplainTypeEntities entities = new ComplainTypeEntities())
                {
                    var entity = entities.COMPLAINTYPES.FirstOrDefault(e => e.ComplaintypeId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ComplainType doesnt exist");
                    else
                    {
                        entities.COMPLAINTYPES.Remove(entity);
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
        public HttpResponseMessage put(int id, COMPLAINTYPES complaintype)
        {
            try
            {
                using (ComplainTypeEntities entities = new ComplainTypeEntities())
                {
                    var entity = entities.COMPLAINTYPES.FirstOrDefault(e => e.ComplaintypeId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ComplainType doesnt exist");
                    else
                    {
                        entity.ComplaintypeId = complaintype.ComplaintypeId;
                        entity.ComplainType = complaintype.ComplainType;
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
