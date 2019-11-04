using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.Http.OData.Routing;
using WebAPI.Models;
using Microsoft.Data.OData;
using System.Data.Entity;
using System.Web.OData.Routing;
using WebAPI.Helper;
namespace WebAPI.API.OdataAPI
{

    public class CustomersController : ODataController
    {

        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        WebAPIContext db = new WebAPIContext();
        // GET: odata/Customers
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<CustomerViewModel> queryOptions)
        {
            try
            {
                queryOptions.Validate(_validationSettings);
                var result = db.Customers.OrderByDescending(i => i.Id).Select(s => new CustomerViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Email,
                    Phone = s.Phone,
                    NormalizeName = s.NormalizeName,
                    CustomerCode = s.CustomerCode
                });
                return Ok(result);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableQuery]
        // GET: odata/Customers(5)
        public IHttpActionResult Get([FromODataUri] int key, ODataQueryOptions<CustomerViewModel> queryOptions)
        {

            try
            {
                queryOptions.Validate(_validationSettings);
                var result = db.Customers.Where(i => i.Id == key).Select(s => new CustomerViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Email,
                    Phone = s.Phone,
                    CustomerCode = s.CustomerCode
                }).FirstOrDefault();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }

        }


        // PUT: odata/Customers(5)
        public IHttpActionResult Put([FromODataUri] int key, CustomerViewModel model)
        {
            var customer = db.Customers.Where(i => i.Id == key).FirstOrDefault();
            customer.Id = model.Id;
            customer.Name = model.Name;
            customer.NormalizeName = Helper.Helper.ConvertToNormalize(model.Name);
            customer.Address = model.Address;
            customer.Email = model.Email;
            customer.Phone = model.Phone;
            if (string.IsNullOrEmpty(model.CustomerCode))
            {
                customer.CustomerCode = Helper.Helper.GenerateCode(DateTime.Now, 1);
                if (db.Customers.Where(i => i.CustomerCode == customer.CustomerCode).FirstOrDefault()!=null)
                {
                    customer.CustomerCode = Helper.Helper.GenerateCode(DateTime.Now, 1);
                }
            }
            else
            {
                var exist = db.Customers.Where(i => i.CustomerCode == model.CustomerCode).FirstOrDefault();
                if (exist == null)
                {
                    customer.CustomerCode = model.CustomerCode;
                }
                else
                {
                    if (customer.Id == exist.Id)
                    {
                        customer.CustomerCode = model.CustomerCode;
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }

            try
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: odata/Customers
        public IHttpActionResult Post(CustomerViewModel model)
        {

            var customer = new Customer()
            {
                Id = model.Id,
                Name = model.Name,
                NormalizeName = Helper.Helper.ConvertToNormalize(model.Name),
                Address = model.Address,
                Email = model.Email,
                Phone = model.Phone,
                CreateDate = DateTime.Now,
            };
            if (string.IsNullOrEmpty(model.CustomerCode))
            {
                customer.CustomerCode = Helper.Helper.GenerateCode(DateTime.Now, 1);
                if (db.Customers.Where(i => i.CustomerCode == customer.CustomerCode).FirstOrDefault() != null)
                {
                    customer.CustomerCode = Helper.Helper.GenerateCode(DateTime.Now, 1);
                }
            }
            else
            {
                var exist = db.Customers.Where(i => i.CustomerCode == model.CustomerCode).FirstOrDefault();
                if (exist == null)
                {
                    customer.CustomerCode = model.CustomerCode;
                }
                else
                {
                    return BadRequest();
                }
            }
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
        // DELETE: odata/Customers(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var customer = db.Customers.Find(key);
            try
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
