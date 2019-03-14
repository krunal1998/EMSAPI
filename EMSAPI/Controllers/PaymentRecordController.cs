using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMSClassLibrary;

namespace EMSAPI.Controllers
{
    public class PaymentRecordController : ApiController
    {
        public HttpResponseMessage get()
        {
            try
            {
                using (PaymentRecordEntities entities = new PaymentRecordEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.PAYMENTRECORD.ToList());
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
                using (PaymentRecordEntities entities = new PaymentRecordEntities())
                {
                    PAYMENTRECORD paymentrecord = entities.PAYMENTRECORD.FirstOrDefault(e => e.PaymentID == id);
                    if (paymentrecord != null)
                        return Request.CreateResponse(HttpStatusCode.OK, paymentrecord);

                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No entity with id " + id + "found!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage post([FromBody] PAYMENTRECORD paymentrecord)
        {
            try
            {
                using (PaymentRecordEntities entities = new PaymentRecordEntities())
                {
                    entities.PAYMENTRECORD.Add(paymentrecord);
                    entities.SaveChanges();
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.Created, paymentrecord);
                    message.Headers.Location = new Uri(Request.RequestUri + paymentrecord.PaymentID.ToString());
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
                using (PaymentRecordEntities entities = new PaymentRecordEntities())
                {
                    var entity = entities.PAYMENTRECORD.FirstOrDefault(e => e.PaymentID == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "PaymentRecord doesnt exist");
                    else
                    {
                        entities.PAYMENTRECORD.Remove(entity);
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
        public HttpResponseMessage put(int id, PAYMENTRECORD paymentrecord)
        {
            try
            {
                using (PaymentRecordEntities entities = new PaymentRecordEntities())
                {
                    var entity = entities.PAYMENTRECORD.FirstOrDefault(e => e.PaymentID == id);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "PaymentRecord doesnt exist");
                    else
                    {
                        entity.PaymentID = paymentrecord.PaymentID;
                        entity.PaymentMonth = paymentrecord.PaymentMonth;
                        entity.PaymentYear = paymentrecord.PaymentYear;
                        entity.JobTitle = paymentrecord.JobTitle;
                        entity.EmployeeId = paymentrecord.EmployeeId;
                        entity.BasicPay = paymentrecord.BasicPay;
                        entity.HRA = paymentrecord.HRA;
                        entity.TA = paymentrecord.TA;
                        entity.OtherAllowances = paymentrecord.OtherAllowances;
                        entity.EPF = paymentrecord.EPF;
                        entity.Tax = paymentrecord.Tax;
                        entity.Absence = paymentrecord.Absence;
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
