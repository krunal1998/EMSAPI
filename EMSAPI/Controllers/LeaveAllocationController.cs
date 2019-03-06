using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class LeaveAllocationController : ApiController
    {
        //Get all allocated leave for all type of jobs
        public HttpResponseMessage GetAll()
        {
            try
            {
                using (LeaveAllocationEntities entity = new LeaveAllocationEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity.LEAVEALLOCATION.ToList());
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //Get allocated leave for specific job tite
        public HttpResponseMessage GetLeaveForJobtitle(int jobtitleid)
        {try
            {
                using (LeaveAllocationEntities entity = new LeaveAllocationEntities())
                {
                    var record= entity.LEAVEALLOCATION.Where(e => e.JobTitleId == jobtitleid).ToList();
                    if (record != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, record);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No record found with id :" + jobtitleid);
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //add new record
        public HttpResponseMessage Post([FromBody] LEAVEALLOCATION leaveallocation)
        {
            try
            {
                using (LeaveAllocationEntities entity = new LeaveAllocationEntities())
                {
                    entity.LEAVEALLOCATION.Add(leaveallocation);
                    entity.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, leaveallocation);
                    message.Headers.Location = new Uri(Request.RequestUri + leaveallocation.JobTitleId.ToString());
                    return message;
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // update allocated leave for specific jobtitle and leavetype
        public HttpResponseMessage put(int jobtitleid, int leavetypeid,[FromBody]LEAVEALLOCATION updatedleave)
        {
            try
            {
                using (LeaveAllocationEntities e = new LeaveAllocationEntities())
                {
                    var entity = e.LEAVEALLOCATION.Find(jobtitleid, leavetypeid);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesn't exist");
                    }
                    else
                    {
                        entity.NumberOfLeave = updatedleave.NumberOfLeave;
                        e.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //delete data
        public HttpResponseMessage Delete(int jobtitleid,int leavetypeid)
        {
            try
            {

                using (LeaveAllocationEntities entity = new LeaveAllocationEntities())
                {
                    var record = entity.LEAVEALLOCATION.Find(jobtitleid, leavetypeid);
                    if (record == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesnt exist");
                    }
                    else
                    {

                        entity.LEAVEALLOCATION.Remove(record);
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
