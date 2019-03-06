using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;
namespace EMSAPI.Controllers
{
    public class LeaveTypeController : ApiController
    {
        public HttpResponseMessage GetAllLeaveType()
        {
            try
            {
                using (LeaveTypeEntities en = new LeaveTypeEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, en.LEAVETYPE.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage GetLeaveType(int id)
        {
            try
            {
                using (LeaveTypeEntities en = new LeaveTypeEntities())
                {
                    var record= en.LEAVETYPE.FirstOrDefault(e => e.LeaveTypeId == id);
                    if (record != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, record);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found with id :" + id);
                    }
                }
            }
            
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //Add Leavetype
        public HttpResponseMessage Post([FromBody] LEAVETYPE leavetype)
        {
            try
            {
                using (LeaveTypeEntities entity = new LeaveTypeEntities())
                {
                    entity.LEAVETYPE.Add(leavetype);
                    entity.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, leavetype);
                    message.Headers.Location = new Uri(Request.RequestUri + leavetype.LeaveTypeId.ToString());
                    return message;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Update Leavetype
        public HttpResponseMessage Put(int id, [FromBody] LEAVETYPE leavetype)
        {
            try
            {
                using (LeaveTypeEntities entity = new LeaveTypeEntities())
                {
                    var en = entity.LEAVETYPE.Find(id);
                    if (en == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesn't exist");
                    }
                    else
                    {
                        en.LeaveTypeName = leavetype.LeaveTypeName;
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

        //Delete Leavetype
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (LeaveTypeEntities entity = new LeaveTypeEntities())
                {
                    var record = entity.LEAVETYPE.FirstOrDefault(e => e.LeaveTypeId == id);
                    if (record == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesnt exist");
                    }
                    else
                    {
                        entity.LEAVETYPE.Remove(record);
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
