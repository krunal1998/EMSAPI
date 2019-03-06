using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;
namespace EMSAPI.Controllers
{
    public class AllowancesController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (AllowancesEntities entities = new AllowancesEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.ALLOWANCES.ToList());
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
                using (AllowancesEntities entities = new AllowancesEntities())
                {
                    ALLOWANCES allowances = entities.ALLOWANCES.FirstOrDefault(e => e.AllowanceId == id);
                    if (allowances != null)
                        return Request.CreateResponse(HttpStatusCode.OK, allowances);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] ALLOWANCES allowances)
        {
            try
            {
                using (AllowancesEntities entities = new AllowancesEntities())
                {
                    entities.ALLOWANCES.Add(allowances);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, allowances);
                    message.Headers.Location = new Uri(Request.RequestUri + allowances.AllowanceId.ToString());
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
                using (AllowancesEntities entities = new AllowancesEntities())
                {
                    var entity = entities.ALLOWANCES.FirstOrDefault(e => e.AllowanceId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Allowances doesnt exist");
                    else
                    {
                        entities.ALLOWANCES.Remove(entity);
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
        public HttpResponseMessage put(int id, ALLOWANCES allowances)
        {
            try
            {
                using (AllowancesEntities entities = new AllowancesEntities())
                {
                    var entity = entities.ALLOWANCES.FirstOrDefault(e => e.AllowanceId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Allowances doesnt exist");
                    else
                    {
                        entity.AllowanceId = allowances.AllowanceId;
                        entity.AllowanceName = allowances.AllowanceName;
                     

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
