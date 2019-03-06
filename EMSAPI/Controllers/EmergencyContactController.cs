using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class EmergencyContactController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (EmergencyContactEntities entities = new EmergencyContactEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.EMERGENCYCONTACT.ToList());
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
                using (EmergencyContactEntities entities = new EmergencyContactEntities())
                {
                    EMERGENCYCONTACT emergencycontact = entities.EMERGENCYCONTACT.FirstOrDefault(e => e.EmergencyContactId == id);
                    if (emergencycontact != null)
                        return Request.CreateResponse(HttpStatusCode.OK, emergencycontact);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] EMERGENCYCONTACT emergencycontact)
        {
            try
            {
                using (EmergencyContactEntities entities = new EmergencyContactEntities())
                {
                    entities.EMERGENCYCONTACT.Add(emergencycontact);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, emergencycontact);
                    message.Headers.Location = new Uri(Request.RequestUri + emergencycontact.EmergencyContactId.ToString());
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
                using (EmergencyContactEntities entities = new EmergencyContactEntities())
                {
                    var entity = entities.EMERGENCYCONTACT.FirstOrDefault(e => e.EmergencyContactId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "EmergencyContact doesnt exist");
                    else
                    {
                        entities.EMERGENCYCONTACT.Remove(entity);
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
        public HttpResponseMessage put(int id, EMERGENCYCONTACT emergencycontact)
        {
            try
            {
                using (EmergencyContactEntities entities = new EmergencyContactEntities())
                {
                    var entity = entities.EMERGENCYCONTACT.FirstOrDefault(e => e.EmergencyContactId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "EmergencyContact doesnt exist");
                    else
                    {
                        entity.EmergencyContactId= emergencycontact.EmergencyContactId;
                        entity.Name = emergencycontact.Name;
                        entity.Relation = emergencycontact.Relation;
                        entity.Telephone = emergencycontact.Telephone;
                        entity.Mobile = emergencycontact.Mobile;

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
