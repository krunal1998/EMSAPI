using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class ProjectsController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (ProjectEntities entities = new ProjectEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.PROJECT.ToList());
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
                using (ProjectEntities entities = new ProjectEntities())
                {
                    PROJECT project = entities.PROJECT.FirstOrDefault(e => e.ProjectId == id);
                    if (project != null)
                        return Request.CreateResponse(HttpStatusCode.OK, project);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] PROJECT project)
        {
            try
            {
                using (ProjectEntities entities = new ProjectEntities())
                {
                    entities.PROJECT.Add(project);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, project);
                    message.Headers.Location = new Uri(Request.RequestUri + project.ProjectId.ToString());
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
                using (ProjectEntities entities = new ProjectEntities())
                {
                    var entity = entities.PROJECT.FirstOrDefault(e => e.ProjectId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Project doesnt exist");
                    else
                    {
                        entities.PROJECT.Remove(entity);
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
        public HttpResponseMessage put(int id, PROJECT project)
        {
            try
            {
                using (ProjectEntities entities = new ProjectEntities())
                {
                    var entity = entities.PROJECT.FirstOrDefault(e => e.ProjectId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Project doesnt exist");
                    else
                    {
                        entity.StartDate = project.StartDate;
                        entity.ProjectTitle = project.ProjectTitle;
                        entity.DueDate = project.DueDate;
                        entity.Detail = project.Detail;
                        entity.SupervisorId = project.SupervisorId;
                        entity.Status = project.Status;
                            
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
