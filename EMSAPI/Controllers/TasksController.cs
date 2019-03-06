using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class TasksController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (TaskEntities entities = new TaskEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.TASK.ToList());
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
                using (TaskEntities entities = new TaskEntities())
                {
                    TASK task = entities.TASK.FirstOrDefault(e => e.TaskId == id);
                    if (task != null)
                        return Request.CreateResponse(HttpStatusCode.OK, task);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] TASK task)
        {
            try
            {
                using (TaskEntities entities = new TaskEntities())
                {
                    entities.TASK.Add(task);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, task);
                    message.Headers.Location = new Uri(Request.RequestUri + task.TaskId.ToString());
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
                using (TaskEntities entities = new TaskEntities())
                {
                    var entity = entities.TASK.FirstOrDefault(e => e.TaskId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Task doesnt exist");
                    else
                    {
                        entities.TASK.Remove(entity);
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
        public HttpResponseMessage put(int id, TASK task)
        {
            try
            {
                using (TaskEntities entities = new TaskEntities())
                {
                    var entity = entities.TASK.FirstOrDefault(e => e.TaskId == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Task doesnt exist");
                    else
                    {
                        entity.AssignDate = task.AssignDate;
                        entity.DueDate = task.DueDate;
                        entity.ProjectId = task.ProjectId;
                        entity.TaskName = task.TaskName;
                        entity.Status = task.Status;

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
