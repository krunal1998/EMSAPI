using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class DocumentsController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (DocumentsEntities entities = new DocumentsEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.DOCUMENTS.ToList());
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
                using (DocumentsEntities entities = new DocumentsEntities())
                {
                    DOCUMENTS documents = entities.DOCUMENTS.FirstOrDefault(e => e.DocumentId == id);
                    if (documents != null)
                        return Request.CreateResponse(HttpStatusCode.OK, documents);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] DOCUMENTS documents)
        {
            try
            {
                using (DocumentsEntities entities = new DocumentsEntities())
                {
                    entities.DOCUMENTS.Add(documents);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, documents);
                    message.Headers.Location = new Uri(Request.RequestUri + documents.DocumentId.ToString());
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
                using (DocumentsEntities entities = new DocumentsEntities())
                {
                    var entity = entities.DOCUMENTS.FirstOrDefault(e => e.DocumentId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Documents doesnt exist");
                    else
                    {
                        entities.DOCUMENTS.Remove(entity);
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
        public HttpResponseMessage put(int id, DOCUMENTS documents)
        {
            try
            {
                using (DocumentsEntities entities = new DocumentsEntities())
                {
                    var entity = entities.DOCUMENTS.FirstOrDefault(e => e.DocumentId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Documents doesnt exist");
                    else
                    {
                        entity.DocumentId = documents.DocumentId;
                        entity.EmployeeId = documents.EmployeeId;
                        entity.DocumentTitle = documents.DocumentTitle;
                        entity.DocumentPhoto = documents.DocumentPhoto;
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
