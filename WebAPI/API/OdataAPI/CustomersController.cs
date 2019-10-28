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
                var result = db.Customers.Select(s => new CustomerViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Email,
                    Phone = s.Phone,
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
                var result = db.Customers.Select(s => new CustomerViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Email,
                    Phone = s.Phone,
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
            var customer = new Customer()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                Phone = model.Phone
            };
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
                Address = model.Address,
                Email = model.Email,
                Phone = model.Phone
            };
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
