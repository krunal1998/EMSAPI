using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class SupervisorController : ApiController
    {
        //get all supervisor list
        public HttpResponseMessage GetAllSupervisor()
        {
            try
            {
                using (SupervisorEntities en = new SupervisorEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, en.SUPERVISOR.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        //get supervisor by id
        public HttpResponseMessage Getsupervisor(int id)
        {
            try
            {
                using (SupervisorEntities en = new SupervisorEntities())
                {
                    SUPERVISOR sup = en.SUPERVISOR.FirstOrDefault(e => e.SupervisorId == id);
                    if (sup != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, sup);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity found with id :" + id);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
        //Add supervisor
        public HttpResponseMessage Post([FromBody] SUPERVISOR supervisor)
        {   try
            {
                using (SupervisorEntities entity = new SupervisorEntities())
                {
                    entity.SUPERVISOR.Add(supervisor);
                    entity.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, supervisor);
                    message.Headers.Location = new Uri(Request.RequestUri + supervisor.SupervisorId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        //Update supervisor
        public HttpResponseMessage Put(int id, [FromBody] SUPERVISOR supervisor)
        {
            try
            {
                using (SupervisorEntities entity = new SupervisorEntities())
                {
                    var en = entity.SUPERVISOR.Find(id);
                    if (en == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesn't exist");
                    }
                    else
                    {
                        en.EmployeeId = supervisor.EmployeeId;
                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        //Delete supervisor
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (SupervisorEntities entity = new SupervisorEntities())
                {
                    var record = entity.SUPERVISOR.FirstOrDefault(e => e.SupervisorId == id);
                    if (record == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesnt exist");
                    }
                    else
                    {
                        entity.SUPERVISOR.Remove(record);
                        entity.SaveChanges();
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
