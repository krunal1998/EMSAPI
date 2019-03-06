using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class GeneratedReviewsController : ApiController
    {

        public HttpResponseMessage get()
        {
            try
            {
                GeneratedReviewEntities entities = new GeneratedReviewEntities();

                {
                    //List<GENERATEREVIEW> _getdata = DB.getallEmp();
                    //List<GENERATEREVIEW> _data = entities.GENERATEREVIEW.ToList();
                    //return Request.CreateResponse();
                    return Request.CreateResponse(HttpStatusCode.OK, entities.GENERATEREVIEW.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }






        //public HttpResponseMessage get()
        //{
        //    try
        //    {
        //        using (GeneratedReviewEntities entities = new GeneratedReviewEntities())
        //        {
        //            var getdata = entities.GENERATEREVIEW.ToList();

        //            return Request.CreateResponse(HttpStatusCode.OK, entities.GENERATEREVIEW.ToList());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        public HttpResponseMessage get(int id)
        {
            try
            {
                using (GeneratedReviewEntities entities = new GeneratedReviewEntities())
                {
                    GENERATEREVIEW gr = entities.GENERATEREVIEW.FirstOrDefault(e => e.GenerateReviewId == id);
                    if (gr != null)
                        return Request.CreateResponse(HttpStatusCode.OK, gr);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] GENERATEREVIEW gr)
        {
            try
            {
                using (GeneratedReviewEntities entities = new GeneratedReviewEntities())
                {
                    entities.GENERATEREVIEW.Add(gr);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, gr);
                    message.Headers.Location = new Uri(Request.RequestUri + gr.GenerateReviewId.ToString());
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
                using (GeneratedReviewEntities entities = new GeneratedReviewEntities())
                {
                    var entity = entities.GENERATEREVIEW.FirstOrDefault(e => e.GenerateReviewId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "GR doesnt exist");
                    else
                    {
                        entities.GENERATEREVIEW.Remove(entity);
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
        public HttpResponseMessage put(int id, GENERATEREVIEW gr)
        {
            try
            {
                using (GeneratedReviewEntities entities = new GeneratedReviewEntities())
                {
                    var entity = entities.GENERATEREVIEW.FirstOrDefault(e => e.GenerateReviewId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "GR doesnt exist");
                    else
                    {
                        entity.DueDate = gr.DueDate;
                        entity.StartDate = gr.StartDate;
                        entity.EndDate = gr.EndDate;
                        entity.EmployeeId = gr.EmployeeId;
                        entity.Status = gr.Status;

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
