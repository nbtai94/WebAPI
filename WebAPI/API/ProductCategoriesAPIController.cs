using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace WebAPI.API
{
    public class ProductCategoriesAPIController : ApiController
    {
        private readonly WebAPIContext db = new WebAPIContext();
        [HttpGet]
        public IHttpActionResult ProductCategories(int skip, int take)
        {
            var result = db.ProductCategories.OrderBy(i => i.Id)
                .Skip(skip)
                .Take(take)
                .Select(s => new ProductCategoryViewModel
                {
                    Id=s.Id,
                    CategoryCode = s.CategoryCode,
                    CategoryName = s.CategoryName,
                    CreateDate = s.CreateDate
                });
            int total = db.ProductCategories.Count();
            return Ok(new
            {
                data = result,
                total = total
            });
        }
        [HttpGet]
        public IHttpActionResult ProductCategory(int Id)
        {
            var result = db.ProductCategories.Find(Id);
            if (result != null)
            {
                return Ok(new { data = result });
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost]

        public IHttpActionResult ProductCategories(ProductCategoryViewModel model)
        {
            if (model.CategoryCode != null && model.CategoryName != null)
            {
                var category = new ProductCategory
                {
                    CategoryCode = model.CategoryCode,
                    CategoryName = model.CategoryName,
                    CreateDate = DateTime.Now
                };
                db.ProductCategories.Add(category);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpDelete]
        public IHttpActionResult ProductCategories(int Id)
        {

            var category = db.ProductCategories.Find(Id);
            if (category != null)
            {
                db.ProductCategories.Remove(category);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }



        }
        [HttpPut]
        public IHttpActionResult ProductCategory(ProductCategoryViewModel model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }

            return Ok();
        }
    }
}
