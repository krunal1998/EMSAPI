using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class ProjectEmployeesController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (ProjectEmployeesEntities entities = new ProjectEmployeesEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.PROJECTEMPLOYEES.ToList());
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
                using (ProjectEmployeesEntities entities = new ProjectEmployeesEntities())
                {
                    PROJECTEMPLOYEES projectemployees = entities.PROJECTEMPLOYEES.FirstOrDefault(e => e.EmployeeId == EmployeeId);
                    if (projectemployees != null)
                        return Request.CreateResponse(HttpStatusCode.OK, projectemployees);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + EmployeeId + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage get(int ProjectId)
        {
            try
            {
                using (ProjectEmployeesEntities entities = new ProjectEmployeesEntities())
                {
                    PROJECTEMPLOYEES projectemployees = entities.PROJECTEMPLOYEES.FirstOrDefault(e => e.ProjectId == ProjectId);
                    if (projectemployees != null)
                        return Request.CreateResponse(HttpStatusCode.OK, projectemployees);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + ProjectId + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] PROJECTEMPLOYEES projectemployees)
        {
            try
            {
                using (ProjectEmployeesEntities entities = new ProjectEmployeesEntities())
                {
                    entities.PROJECTEMPLOYEES.Add(projectemployees);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, projectemployees);
                    message.Headers.Location = new Uri(Request.RequestUri +"?ProjectId=" +projectemployees.ProjectId.ToString()+ "&EmployeeId="+projectemployees.EmployeeId);
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage delete(int ProjectId, string EmployeeId)
        {
            try
            {
                using (ProjectEmployeesEntities entities = new ProjectEmployeesEntities())
                {
                    var entity = entities.PROJECTEMPLOYEES.FirstOrDefault(e => e.ProjectId == ProjectId && e.EmployeeId == EmployeeId);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ProjectEmployees doesnt exist");
                    else
                    {
                        entities.PROJECTEMPLOYEES.Remove(entity);
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

        public HttpResponseMessage put(int ProjectId,string EmployeeId, PROJECTEMPLOYEES projectemployees)
        {
            try
            {
                using (ProjectEmployeesEntities entities = new ProjectEmployeesEntities())
                {
                    var entity = entities.PROJECTEMPLOYEES.FirstOrDefault(e => e.ProjectId == ProjectId && e.EmployeeId == EmployeeId);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ProjectEmployees doesnt exist");
                    else
                    {
                        entity.EmployeeId = projectemployees.EmployeeId;
                        entity.ProjectId = ProjectId;
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
