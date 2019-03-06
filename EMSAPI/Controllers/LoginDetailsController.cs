using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class LoginDetailsController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (LoginDetailsEntities entities = new LoginDetailsEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.LOGINDETAILS.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage get(string id)
        {
            try
            {
                using (LoginDetailsEntities entities = new LoginDetailsEntities())
                {
                    LOGINDETAILS logindetails = entities.LOGINDETAILS.FirstOrDefault(e => e.EmployeeId == id);
                    if (logindetails != null)
                        return Request.CreateResponse(HttpStatusCode.OK, logindetails);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] LOGINDETAILS logindetails)
        {
            try
            {
                using (LoginDetailsEntities entities = new LoginDetailsEntities())
                {
                    entities.LOGINDETAILS.Add(logindetails);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, logindetails);
                    message.Headers.Location = new Uri(Request.RequestUri + logindetails.EmployeeId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage delete(string id)
        {
            try
            {
                using (LoginDetailsEntities entities = new LoginDetailsEntities())
                {
                    var entity = entities.LOGINDETAILS.FirstOrDefault(e => e.EmployeeId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "LoginDetails doesnt exist");
                    else
                    {
                        entities.LOGINDETAILS.Remove(entity);
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
        public HttpResponseMessage put(string id, LOGINDETAILS logindetails)
        {
            try
            {
                using (LoginDetailsEntities entities = new LoginDetailsEntities())
                {
                    var entity = entities.LOGINDETAILS.FirstOrDefault(e => e.EmployeeId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "LoginDetails doesnt exist");
                    else
                    {
                        entity.EmployeeId = logindetails.EmployeeId;
                        entity.Password = logindetails.Password;
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
