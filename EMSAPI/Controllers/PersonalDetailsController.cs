using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class PersonalDetailsController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (PersonalDetailsEntities entities = new PersonalDetailsEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.PERSONALDETAILS.ToList());
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
                using (PersonalDetailsEntities entities = new PersonalDetailsEntities())
                {
                    PERSONALDETAILS personaldetails = entities.PERSONALDETAILS.FirstOrDefault(e => e.PersonalDetailId == id);
                    if (personaldetails != null)
                        return Request.CreateResponse(HttpStatusCode.OK, personaldetails);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] PERSONALDETAILS personaldetails)
        {
            try
            {
                using (PersonalDetailsEntities entities = new PersonalDetailsEntities())
                {
                    entities.PERSONALDETAILS.Add(personaldetails);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, personaldetails);
                    message.Headers.Location = new Uri(Request.RequestUri + personaldetails.PersonalDetailId.ToString());
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
                using (PersonalDetailsEntities entities = new PersonalDetailsEntities())
                {
                    var entity = entities.PERSONALDETAILS.FirstOrDefault(e => e.PersonalDetailId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "PersonalDetails doesnt exist");
                    else
                    {
                        entities.PERSONALDETAILS.Remove(entity);
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
        public HttpResponseMessage put(int id, PERSONALDETAILS personaldetails)
        {
            try
            {
                using (PersonalDetailsEntities entities = new PersonalDetailsEntities())
                {
                    var entity = entities.PERSONALDETAILS.FirstOrDefault(e => e.PersonalDetailId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "PersonalDetails doesnt exist");
                    else
                    {
                        entity.PersonalDetailId = personaldetails.PersonalDetailId;
                        entity.FirstName = personaldetails.FirstName;
                        entity.MiddleName = personaldetails.MiddleName;
                        entity.LastName = personaldetails.LastName;
                        entity.Gender = personaldetails.Gender;
                        entity.MartialStatus = personaldetails.MartialStatus;
                        entity.Nationality = personaldetails.Nationality;
                        entity.DateOfBirth = personaldetails.DateOfBirth;


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
