﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CustomersController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: api/Customers
        public IHttpActionResult GetCustomers(int skip, int take)
        {
            var total = db.Customers.Count();
            var result = db.Customers.OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take)
                .Select(s => new CustomerViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Address,
                    Phone = s.Phone
                });
            return Ok(new
            {
                data = result,
                total = total,
                skip = skip,
                take = take
            });
        }
        [HttpGet]
        public IHttpActionResult SearchCustomer(string key)
        {

            var result = db.Customers.OrderBy(x => x.Id).Where(x => x.Name.Contains(key) || x.Address.Contains(key) || x.Email.Contains(key) || key == null)
                .Select(s => new CustomerViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Email,
                    Phone = s.Phone
                });
            var total = result.Count();
            return Ok(new
            {
                data = result,
                total = total,
            });
        }

        //POST: AddCUstomer
        [HttpPost]
        public IHttpActionResult AddCustomer(Customer customer)
        {
            if (customer != null)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public IHttpActionResult GetCustomer(int Id)
        {
            var result = db.Customers.Where(i => i.Id == Id).SingleOrDefault();
            return Ok(result);
        }

        //PUT api/Customers/Edit
        [HttpPut]
        public IHttpActionResult EditCustomer(int Id,Customer customer)
        {
            var cus = db.Customers.Where(i => i.Id == Id).SingleOrDefault();
            cus.Name = customer.Name;
            cus.Address = customer.Address;
            cus.Phone = customer.Phone;
            cus.Email = customer.Email;
            db.SaveChanges();
            return Ok();
        }
     



        // DELETE: api/Customers/5
      [HttpDelete]
      public IHttpActionResult RemoveCustomer(int Id)
        {
            Customer cus = db.Customers.Where(i => i.Id == Id).SingleOrDefault();
            if (cus != null)
            {
                db.Customers.Remove(cus);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.Id == id) > 0;
        }
    }
}