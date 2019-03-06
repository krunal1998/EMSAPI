
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class WeekDayController : ApiController
    {
        public HttpResponseMessage GetAllWeekDay()
        {
            try
            {
                using (WeekDayEntities en = new WeekDayEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, en.WEEKDAY.ToList());
                }
            }
            
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //Get WeekDay using id
        public HttpResponseMessage GetWeekDay(int id)
        {
            try
            {
                using (WeekDayEntities en = new WeekDayEntities())
                {
                    var record= en.WEEKDAY.Where(e => e.WeekDayId == id).ToList();
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
        //Add WeekDay Duration
        public HttpResponseMessage Post([FromBody] WEEKDAY day)
        {
            try
            {
                using (WeekDayEntities entity = new WeekDayEntities())
                {
                    entity.WEEKDAY.Add(day);
                    entity.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created,day);
                    message.Headers.Location = new Uri(Request.RequestUri + day.WeekDayId.ToString());
                    return message;
                }
            }
            
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Update WeekDa Duration
        public HttpResponseMessage Put(int id, [FromBody] WEEKDAY day)
        {
            try
            {
                using (WeekDayEntities entity = new WeekDayEntities())
                {
                    var en = entity.WEEKDAY.Find(id);
                    if (en == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesn't exist");
                    }
                    else
                    {
                        en.WeekDayName = day.WeekDayName;
                        en.WeekDayDuration = day.WeekDayDuration;
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

        //Delete WeekDay 
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (WeekDayEntities entity = new WeekDayEntities())
                {
                    var record = entity.WEEKDAY.FirstOrDefault(e => e.WeekDayId == id);
                    if (record == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "record doesnt exist");
                    }
                    else
                    {
                        entity.WEEKDAY.Remove(record);
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
