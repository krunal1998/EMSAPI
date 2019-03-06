using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (EmployeeEntities entities = new EmployeeEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.EMPLOYEE.ToList());
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
                using (EmployeeEntities entities = new EmployeeEntities())
                {
                    EMPLOYEE employee = entities.EMPLOYEE.FirstOrDefault(e => e.EmployeeId == id);
                    if (employee != null)
                        return Request.CreateResponse(HttpStatusCode.OK, employee);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] EMPLOYEE employee)
        {
            try
            {
                using (EmployeeEntities entities = new EmployeeEntities())
                {
                    entities.EMPLOYEE.Add(employee);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.EmployeeId.ToString());
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
                using (EmployeeEntities entities = new EmployeeEntities())
                {
                    var entity = entities.EMPLOYEE.FirstOrDefault(e => e.EmployeeId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee doesnt exist");
                    else
                    {
                        entities.EMPLOYEE.Remove(entity);
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
        public HttpResponseMessage put(string id, EMPLOYEE employee)
        {
            try
            {
                using (EmployeeEntities entities = new EmployeeEntities())
                {
                    var entity = entities.EMPLOYEE.FirstOrDefault(e => e.EmployeeId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee doesnt exist");
                    else
                    {
                        entity.PersonalDetailId = employee.PersonalDetailId;
                        entity.EmergencyContactId = employee.EmergencyContactId;
                        entity.EmployeeStatus = employee.EmployeeStatus;
                        entity.SupervisorId = employee.SupervisorId;
                        entity.JobtitleId = employee.JobtitleId;

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
