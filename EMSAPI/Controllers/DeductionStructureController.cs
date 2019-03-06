using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;
namespace EMSAPI.Controllers
{
    public class DeductionStructureController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (DeductionStructureEntities entities = new DeductionStructureEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.DEDUCTIONSTRUCTURE.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage get(int id,string employeeid)
        {
            try
            {
                using (DeductionStructureEntities entities = new DeductionStructureEntities())
                {
                    DEDUCTIONSTRUCTURE deductionstructture = entities.DEDUCTIONSTRUCTURE.FirstOrDefault(e => e.DeductionId == id && e.EmployeeId == employeeid);
                    if (deductionstructture != null)
                        return Request.CreateResponse(HttpStatusCode.OK, deductionstructture);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] DEDUCTIONSTRUCTURE deductionstructture)
        {
            try
            {
                using (DeductionStructureEntities entities = new DeductionStructureEntities())
                {
                    entities.DEDUCTIONSTRUCTURE.Add(deductionstructture);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, deductionstructture);
                    message.Headers.Location = new Uri(Request.RequestUri + deductionstructture.DeductionId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage delete(int id,string employeeid)
        {
            try
            {
                using (DeductionStructureEntities entities = new DeductionStructureEntities())
                {
                    var entity = entities.DEDUCTIONSTRUCTURE.FirstOrDefault(e => e.DeductionId == id && e.EmployeeId == employeeid);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "DeductionStructure doesnt exist");
                    else
                    {
                        entities.DEDUCTIONSTRUCTURE.Remove(entity);
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
        public HttpResponseMessage put(int id,string employeeid,DEDUCTIONSTRUCTURE deductionstructture)
        {
            try
            {
                using (DeductionStructureEntities entities = new DeductionStructureEntities())
                {
                    var entity = entities.DEDUCTIONSTRUCTURE.FirstOrDefault(e => e.DeductionId == id && e.EmployeeId == employeeid);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "DeductionStructure doesnt exist");
                    else
                    {
                        entity.DeductionId = deductionstructture.DeductionId;
                        entity.DeductionAmount = deductionstructture.DeductionAmount;
                        entity.EmployeeId = deductionstructture.EmployeeId;                        
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
