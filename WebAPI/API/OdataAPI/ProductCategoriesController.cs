using Microsoft.Data.OData;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using WebAPI.Models;
using WebAPI.ViewModels;
using System.Linq;
using System.Data.Entity;
using System;

namespace WebAPI.API.OdataAPI
{
    public class ProductCategoriesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        WebAPIContext db = new WebAPIContext();
        // GET: odata/ProductCategories

        [EnableQuery]

        public IHttpActionResult Get(ODataQueryOptions<ProductCategoryViewModel> queryOptions)
        {

            try
            {

                queryOptions.Validate(_validationSettings);
                var result = db.ProductCategories.OrderBy(i => i.Id).Select(s => new ProductCategoryViewModel
                {
                    Id = s.Id,
                    CategoryCode = s.CategoryCode,
                    CategoryName = s.CategoryName,
                    CreateDate = s.CreateDate,
                });
                return Ok(result);

            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // GET: odata/ProductCategories(5)
        public IHttpActionResult GetProductCategoryViewModel([FromODataUri] int key, ODataQueryOptions<ProductCategoryViewModel> queryOptions)
        {
            var result = db.ProductCategories
                .Where(i => i.Id == key)
                .Select(s => new ProductCategoryViewModel
                {
                   Id=s.Id,
                   CategoryName=s.CategoryName,
                   CategoryCode=s.CategoryCode,
                   CreateDate=s.CreateDate,
                }).FirstOrDefault();
            return Ok(result);
        }

        // PUT: odata/ProductCategories(5)
        public IHttpActionResult Put([FromODataUri] int key, ProductCategoryViewModel model)
        {
            ProductCategory category = new ProductCategory
            {
                Id=model.Id,
                CategoryCode=model.CategoryCode,
                CategoryName=model.CategoryName,
                CreateDate=model.CreateDate,
            };
            try
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
            return Ok();
        }

        // POST: odata/ProductCategories
        public IHttpActionResult Post(ProductCategoryViewModel model)
        {
            var category = new ProductCategory() {
                Id = model.Id,
                CategoryName = model.CategoryName,
                CategoryCode = model.CategoryCode,
                CreateDate = DateTime.Now,            
            };
            try
            {
                db.ProductCategories.Add(category);
                db.SaveChanges();
            }
            catch 
            {
                return BadRequest();
            }
            return Ok();

        }



        // DELETE: odata/ProductCategories(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var category = db.ProductCategories.Find(key);
            try
            {
                db.ProductCategories.Remove(category);
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