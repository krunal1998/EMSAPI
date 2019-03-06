using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;


namespace EMSAPI.Controllers
{
    public class AttendanceController : ApiController
    {
        // get all data of attendance
        public HttpResponseMessage GetAllData()
        {
            try
            {
                using (AttendanceEntities entity = new AttendanceEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity.ATTENDANCE.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        } 

        //get attendance data for specific employee
        public HttpResponseMessage GetEmployeeAttendance(String employeeid)
        {
            try
            {
                using (AttendanceEntities entity = new AttendanceEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity.ATTENDANCE.Where(e => e.EmployeeId.Equals(employeeid)).ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        //get attendance data for specific date
        public HttpResponseMessage GetDateAttendance(DateTime  date)
        {
            try
            {
                using (AttendanceEntities entity = new AttendanceEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity.ATTENDANCE.Where(e => e.PunchInDate.Equals(date)).ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        
        //add attendance data
        public HttpResponseMessage post([FromBody] ATTENDANCE attendance)
        {
            try
            {
                using (AttendanceEntities entity = new AttendanceEntities())
                {
                    entity.ATTENDANCE.Add(attendance);
                    entity.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, attendance);
                    message.Headers.Location = new Uri(Request.RequestUri + attendance.EmployeeId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        //update attendance record
        public HttpResponseMessage put(String employeeid,DateTime date,[FromBody]ATTENDANCE attendance)
        {
            try
            {
                using (AttendanceEntities entity = new AttendanceEntities())
                {
                    var en = entity.ATTENDANCE.FirstOrDefault(e => (e.EmployeeId.Equals(employeeid) && e.PunchInDate.Equals(date)));
                    if (en == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesn't exist");
                    }
                    else
                    {
                        en.PunchInTime = attendance.PunchInTime;
                        en.PunchOutTime = attendance.PunchOutTime;
                        en.WorkingHours = attendance.WorkingHours;

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

        //delete record
        public HttpResponseMessage delete(String employeeid, DateTime date)
        {
            try
            {
                using (AttendanceEntities entity = new AttendanceEntities())
                {
                    var record = entity.ATTENDANCE.FirstOrDefault(e => (e.EmployeeId.Equals(employeeid) && e.PunchInDate.Equals(date)));
                    if (record == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesnt exist");
                    }
                    else
                    {
                        entity.ATTENDANCE.Remove(record);
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
