using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;
namespace EMSAPI.Controllers
{
    public class LeaveController : ApiController
    {
        //get all leave data
        public HttpResponseMessage GetAllData()
        {
            try
            {
                using (LeaveEntities entity = new LeaveEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity.LEAVES.ToList());
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //get data for specific leave
        public HttpResponseMessage  GetLeaveData(int id)
        {
            try
            {
                using (LeaveEntities entity = new LeaveEntities())
                {
                    LEAVES leave = entity.LEAVES.FirstOrDefault(e => e.LeaveId == id);
                    if (leave != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, leave);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity found with leave id :" + id);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //get leave details for specific employee
        public HttpResponseMessage GetEmpoyeeLeave(String employeeid)
        {
            try
            {
                using (LeaveEntities entity = new LeaveEntities())
                {
                    IEnumerable<LEAVES> leaves = entity.LEAVES.Where(e => (e.EmployeeId.Equals(employeeid))).ToList();
                    if (leaves != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, leaves);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity found with employeeid :" + employeeid);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //get leave record between two dates
        public HttpResponseMessage GetSpecificList(DateTime date1, DateTime date2)
        {
            try
            {
                using (LeaveEntities en = new LeaveEntities())
                {
                    IEnumerable<LEAVES> leaves = en.LEAVES.Where(e => (e.StartDate >= date1 && e.StartDate <= date2)).ToList();
                    if (leaves != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, leaves);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No leave between :" + date1+" and " +date2);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //get leave details for specific Leave Status
        public HttpResponseMessage GetLeavestatus(String status)
        {
            try
            {
                using (LeaveEntities entity = new LeaveEntities())
                {
                    IEnumerable<LEAVES> leaves = entity.LEAVES.Where(e => (e.LeaveStatus.Equals(status))).ToList();
                    if (leaves != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, leaves);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity found with status :" + status);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Add leave data
        public HttpResponseMessage Post([FromBody]LEAVES leave)
        {
            try
            {
                using (LeaveEntities entity = new LeaveEntities())
                {
                    entity.LEAVES.Add(leave);
                    entity.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, leave);
                    message.Headers.Location = new Uri(Request.RequestUri + leave.LeaveId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        //update leave detail
        public HttpResponseMessage Put(int id,[FromBody]LEAVES leave)
        {
            try
            {

                using (LeaveEntities entity = new LeaveEntities())
                {
                    var en = entity.LEAVES.FirstOrDefault(e => e.LeaveId == id);
                    if (en == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "leave record doesn't exist");
                    }
                    else
                    {
                        en.StartDate = leave.StartDate;
                        en.LastDate = leave.LastDate;
                        en.LeavetypeId = leave.LeavetypeId;
                        en.LeaveStatus = leave.LeaveStatus;
                        en.NumberOfDays = leave.NumberOfDays;
                        en.Description = leave.Description;
                        en.LeaveDocumnet = en.LeaveDocumnet;

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

        //Delete leave record
        public HttpResponseMessage delete(int id)
        {
            try
            {
                using (LeaveEntities entity = new LeaveEntities())
                {
                    var record = entity.LEAVES.FirstOrDefault(e => e.LeaveId == id);
                    if (record == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "leave record doesnt exist");
                    }
                    else
                    {
                        entity.LEAVES.Remove(record);
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




