using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class AllocatedAllowancesController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (AllocatedAllowancesEntities entities = new AllocatedAllowancesEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.ALLOCATEDALLOWANCES.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage get(int id,string employeeid )
        {
            try
            {
                using (AllocatedAllowancesEntities entities = new AllocatedAllowancesEntities())
                {
                    ALLOCATEDALLOWANCES allocatedallowances = entities.ALLOCATEDALLOWANCES.FirstOrDefault(e => e.AllowanceId == id && e.EmployeeId == employeeid);

                    if (allocatedallowances != null)
                        return Request.CreateResponse(HttpStatusCode.OK, allocatedallowances);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] ALLOCATEDALLOWANCES allocatedallowances)
        {
            try
            {
                using (AllocatedAllowancesEntities entities = new AllocatedAllowancesEntities())
                {
                    entities.ALLOCATEDALLOWANCES.Add(allocatedallowances);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, allocatedallowances);
                    message.Headers.Location = new Uri(Request.RequestUri + allocatedallowances.AllowanceId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage delete(int id, string employeeid)
        {
            try
            {
                using (AllocatedAllowancesEntities entities = new AllocatedAllowancesEntities())
                {
                    var entity = entities.ALLOCATEDALLOWANCES.FirstOrDefault(e => e.AllowanceId == id && e.EmployeeId == employeeid);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "AllocatedAllowances doesnt exist");
                    else
                    {
                        entities.ALLOCATEDALLOWANCES.Remove(entity);
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
        public HttpResponseMessage put(int id,string employeeid, ALLOCATEDALLOWANCES allocatedallowances)
        {
            try
            {
                using (AllocatedAllowancesEntities entities = new AllocatedAllowancesEntities())
                {
                    var entity = entities.ALLOCATEDALLOWANCES.FirstOrDefault(e => e.AllowanceId == id && e.EmployeeId ==employeeid);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "AllocatedAllowances doesnt exist");
                    else
                    {
                        entity.AllowanceId = allocatedallowances.AllowanceId;
                        entity.EmployeeId = allocatedallowances.EmployeeId;
                        entity.Salary = allocatedallowances.Salary;
                        

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
