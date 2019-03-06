using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Content
{
    public class TaskEmployeesController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (TaskEmployeesEntities entities = new TaskEmployeesEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.TASKEMPLOYEES.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage get(string EmployeeId)
        {
            try
            {
                using (TaskEmployeesEntities entities = new TaskEmployeesEntities())
                {
                    TASKEMPLOYEES taskemployees = entities.TASKEMPLOYEES.FirstOrDefault(e => e.EmployeeId == EmployeeId);
                    if (taskemployees != null)
                        return Request.CreateResponse(HttpStatusCode.OK, taskemployees);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + EmployeeId + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage get(int TaskId)
        {
            try
            {
                using (TaskEmployeesEntities entities = new TaskEmployeesEntities())
                {
                    TASKEMPLOYEES taskemployees = entities.TASKEMPLOYEES.FirstOrDefault(e => e.TaskId == TaskId);
                    if (taskemployees != null)
                        return Request.CreateResponse(HttpStatusCode.OK, taskemployees);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + TaskId + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] TASKEMPLOYEES taskemployees)
        {
            try
            {
                using (TaskEmployeesEntities entities = new TaskEmployeesEntities())
                {
                    entities.TASKEMPLOYEES.Add(taskemployees);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, taskemployees);
                    message.Headers.Location = new Uri(Request.RequestUri + "?TaskId=" + taskemployees.TaskId.ToString() + "&EmployeeId=" + taskemployees.EmployeeId);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage delete(int TaskId, string EmployeeId)
        {
            try
            {
                using (TaskEmployeesEntities entities = new TaskEmployeesEntities())
                {
                    var entity = entities.TASKEMPLOYEES.FirstOrDefault(e => e.TaskId == TaskId && e.EmployeeId == EmployeeId);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "TaskEmployees doesnt exist");
                    else
                    {
                        entities.TASKEMPLOYEES.Remove(entity);
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

        public HttpResponseMessage put(int TaskId, string EmployeeId, TASKEMPLOYEES taskemployees)
        {
            try
            {
                using (TaskEmployeesEntities entities = new TaskEmployeesEntities())
                {
                    var entity = entities.TASKEMPLOYEES.FirstOrDefault(e => e.TaskId == TaskId && e.EmployeeId == EmployeeId);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "TaskEmployees doesnt exist");
                    else
                    {
                        entity.EmployeeId = taskemployees.EmployeeId;
                        entity.TaskId = TaskId;
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
