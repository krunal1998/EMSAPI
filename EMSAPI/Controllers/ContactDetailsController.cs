using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class ContactDetailsController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (ContactDetailsEntities entities = new ContactDetailsEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.CONTACTDETAILS.ToList());
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
                using (ContactDetailsEntities entities = new ContactDetailsEntities())
                {
                    CONTACTDETAILS contactdetails = entities.CONTACTDETAILS.FirstOrDefault(e => e.ContactId == id);
                    if (contactdetails != null)
                        return Request.CreateResponse(HttpStatusCode.OK, contactdetails);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] CONTACTDETAILS contactdetails)
        {
            try
            {
                using (ContactDetailsEntities entities = new ContactDetailsEntities())
                {
                    entities.CONTACTDETAILS.Add(contactdetails);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, contactdetails);
                    message.Headers.Location = new Uri(Request.RequestUri + contactdetails.ContactId.ToString());
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
                using (ContactDetailsEntities entities = new ContactDetailsEntities())
                {
                    var entity = entities.CONTACTDETAILS.FirstOrDefault(e => e.ContactId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ContactDetails doesnt exist");
                    else
                    {
                        entities.CONTACTDETAILS.Remove(entity);
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
        public HttpResponseMessage put(int id, CONTACTDETAILS contactdetails)
        {
            try
            {
                using (ContactDetailsEntities entities = new ContactDetailsEntities())
                {
                    var entity = entities.CONTACTDETAILS.FirstOrDefault(e => e.ContactId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ContactDetails doesnt exist");
                    else
                    {
                        entity.ContactId = contactdetails.ContactId;
                        entity.Street = contactdetails.Street;
                        entity.Landmark = contactdetails.Landmark;
                        entity.City = contactdetails.City;
                        entity.State = contactdetails.State;
                        entity.Pincode = contactdetails.Pincode;
                        entity.Country = contactdetails.Country;
                        entity.Telephone = contactdetails.Telephone;
                        entity.Mobile = contactdetails.Mobile;
                        entity.Email = contactdetails.Email;

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
