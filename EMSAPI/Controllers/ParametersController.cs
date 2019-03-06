using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class ParametersController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (PerformanceParameterEntities entities = new PerformanceParameterEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.PERFORMANCEPARAMETER.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage get(int id)
        {
            try
            {
                using (PerformanceParameterEntities entities = new PerformanceParameterEntities())
                {
                    PERFORMANCEPARAMETER parameter = entities.PERFORMANCEPARAMETER.FirstOrDefault(e => e.ParameterId == id);
                    if (parameter != null)
                        return Request.CreateResponse(HttpStatusCode.OK, parameter);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] PERFORMANCEPARAMETER parameter)
        {
            try
            {
                using (PerformanceParameterEntities entities = new PerformanceParameterEntities())
                {
                    entities.PERFORMANCEPARAMETER.Add(parameter);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, parameter);
                    message.Headers.Location = new Uri(Request.RequestUri + parameter.ParameterId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage delete(int id)
        {
            try
            {
                using (PerformanceParameterEntities entities = new PerformanceParameterEntities())
                {
                    var entity = entities.PERFORMANCEPARAMETER.FirstOrDefault(e => e.ParameterId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "parameter doesnt exist");
                    else
                    {
                        entities.PERFORMANCEPARAMETER.Remove(entity);
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
        public HttpResponseMessage put(int id, PERFORMANCEPARAMETER parameter)
        {
            try
            {
                using (PerformanceParameterEntities entities = new PerformanceParameterEntities())
                {
                    var entity = entities.PERFORMANCEPARAMETER.FirstOrDefault(e => e.ParameterId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "parameter doesnt exist");
                    else
                    {
                        entity.JobTitleId = parameter.JobTitleId;
                        entity.ParameterName = parameter.ParameterName;
                        entity.MinRating = parameter.MinRating;
                        entity.MaxRating = parameter.MaxRating;

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

