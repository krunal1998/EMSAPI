using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class EmployeeLeavesController : ApiController
    {
        // get all leave data
        public HttpResponseMessage GetAllLeave()
        {
            try
            {
                using (EmployeeLeaveEntities entity = new EmployeeLeaveEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity.EMPLOYEELEAVES.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        //get leave data for specific employee
        public HttpResponseMessage GetEmployeeLeave(string employeeid)
        {
            try
            {
                using (EmployeeLeaveEntities entity = new EmployeeLeaveEntities())
                {
                    return Request.CreateResponse( entity.EMPLOYEELEAVES.Where(e => (e.EmployeeId.Equals(employeeid))).ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        //get leave data for specific leave type
        public HttpResponseMessage GetEmployeeLeave(int leavetypeid)
        {
            try
            {
                using (EmployeeLeaveEntities entity = new EmployeeLeaveEntities())
                {
                    return Request.CreateResponse(entity.EMPLOYEELEAVES.Where(e => e.LeaveTypeId == leavetypeid).ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage GetEmployeeLeave(string employeeid, int leavetypeid)
        {
            try
            {
                using (EmployeeLeaveEntities entity = new EmployeeLeaveEntities())
                {
                    return Request.CreateResponse(entity.EMPLOYEELEAVES.Where(e => (e.EmployeeId.Equals(employeeid) & e.LeaveTypeId == leavetypeid)).ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        //Add record
        public HttpResponseMessage Post([FromBody]EMPLOYEELEAVES employeeleave)
        {
            try
            {
                using (EmployeeLeaveEntities entity = new EmployeeLeaveEntities())
                {
                    entity.EMPLOYEELEAVES.Add(employeeleave);
                    entity.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, employeeleave);
                    message.Headers.Location = new Uri(Request.RequestUri + employeeleave.EmployeeId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        //update employee leave record
        public HttpResponseMessage Put(String employeeid,int leavetypeid,[FromBody]EMPLOYEELEAVES employeeleave)
        {
            try
            {
                using (EmployeeLeaveEntities entity = new EmployeeLeaveEntities())
                {
                    var en = entity.EMPLOYEELEAVES.Find(employeeid, leavetypeid);
                    if (en == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "leave record doesn't exist");
                    }
                    else
                    {
                        en.NumberOfLeaves = employeeleave.NumberOfLeaves;
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

        //delete specific record
        public HttpResponseMessage Delete(String employeeid, int leavetypeid, [FromBody]EMPLOYEELEAVES employeeleave)
        {
            try
            {
                using (EmployeeLeaveEntities entity = new EmployeeLeaveEntities())
                {
                    var record = entity.EMPLOYEELEAVES.FirstOrDefault(e => (e.EmployeeId.Equals(employeeid) && e.LeaveTypeId == leavetypeid));
                    if (record == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesnt exist");
                    }
                    else
                    {
                        entity.EMPLOYEELEAVES.Remove(record);
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
