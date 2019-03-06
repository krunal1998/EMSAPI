

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;
namespace EMSAPI.Controllers
{
    public class ReviewsController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (ReviewEntities entities = new ReviewEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.REVIEW.ToList());
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        public HttpResponseMessage get(int id)
        {
            try {         
                using (ReviewEntities entities = new ReviewEntities())
                {
                    REVIEW review = entities.REVIEW.FirstOrDefault(e => e.ReviewId == id);
                    if (review != null)
                        return Request.CreateResponse(HttpStatusCode.OK, review);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] REVIEW review)
        {
            try
            {
                using (ReviewEntities entities = new ReviewEntities())
                {
                    entities.REVIEW.Add(review);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, review);
                    message.Headers.Location = new Uri(Request.RequestUri + review.ReviewId.ToString());
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
                using (ReviewEntities entities = new ReviewEntities())
                {
                    var entity = entities.REVIEW.FirstOrDefault(e => e.ReviewId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Review doesnt exist");
                    else
                    {
                        entities.REVIEW.Remove(entity);
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
        public HttpResponseMessage put(int id, REVIEW review)
        {
            try
            {
                using (ReviewEntities entities = new ReviewEntities())
                {
                    var entity = entities.REVIEW.FirstOrDefault(e => e.ReviewId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Review doesnt exist");
                    else
                    {
                        entity.Rating = review.Rating;
                        entity.Comment = review.Comment;
                        entity.GenerateReviewId = review.GenerateReviewId;
                        entity.EmployeeId = review.EmployeeId;
                        entity.PerameterId = review.PerameterId;

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
