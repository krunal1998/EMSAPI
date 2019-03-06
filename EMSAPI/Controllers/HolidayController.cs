using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;
namespace EMSAPI.Controllers
{
    public class HolidayController : ApiController
    {
        public HttpResponseMessage GetAllHoliday()
        {
            try
            {
                using (HolidayEntities en = new HolidayEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, en.HOLIDAYS.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage GetSpecificList(DateTime  date1, DateTime date2)
        {
            try
            {
                using (HolidayEntities en = new HolidayEntities())
                {
                    IEnumerable<HOLIDAYS> holidays = en.HOLIDAYS.Where(e => (e.HolidayDate >= date1 && e.HolidayDate <= date2)).ToList();
                    if (holidays != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, holidays);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No holiday found between:" + date1+" and "+date2);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        
        public HttpResponseMessage Post([FromBody] HOLIDAYS h)
        {
            try
            {
                using (HolidayEntities e = new HolidayEntities())
                {
                    e.HOLIDAYS.Add(h);
                    e.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, h);
                    message.Headers.Location = new Uri(Request.RequestUri + h.HolidayId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Put(int id,[FromBody] HOLIDAYS h)
        {
            try
            {
                using (HolidayEntities e = new HolidayEntities())
                {
                    var entity = e.HOLIDAYS.Find(id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No holiday exist");
                    }
                    else
                    {

                        entity.HolidayDate = h.HolidayDate;
                        entity.HolidayName = h.HolidayName;
                        entity.WorkDuration = h.WorkDuration;
                        entity.RepeatedAnnually = h.RepeatedAnnually;
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
         public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (HolidayEntities entity = new HolidayEntities())
                {
                    var record = entity.HOLIDAYS.FirstOrDefault(e => e.HolidayId == id);
                    if (record == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "leave record doesnt exist");
                    }
                    else
                    {
                        entity.HOLIDAYS.Remove(record);
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


   