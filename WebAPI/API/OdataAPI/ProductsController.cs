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
using WebAPI.ViewModels;
using Microsoft.Data.OData;
using WebAPI.Models;
using System.Data.Entity;

namespace WebAPI.API.OdataAPI
{

    public class ProductsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        WebAPIContext db = new WebAPIContext();
        // GET: odata/Products
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<ProductViewModel> queryOptions)
        {

            try
            {
                queryOptions.Validate(_validationSettings);
                var result = db.Products.Select(s => new ProductViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    ProductCode = s.ProductCode,
                    ProductCategoryName = s.ProductCategory.CategoryName,
                    Price = s.Price,
                    Description = s.Description,
                });
                return Ok(result);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // GET: odata/Products(5)
        [EnableQuery]

        public IHttpActionResult Get([FromODataUri] int key, ODataQueryOptions<ProductViewModel> queryOptions)
        {
            var result = db.Products
                .Where(i => i.Id == key)
                .Select(s => new ProductViewModel
                {
                    Id = s.Id,
                    ProductCategoryName=s.ProductCategory.CategoryName,
                    Name = s.Name,
                    Price = s.Price,
                    ProductCode = s.ProductCode,
                    Description = s.Description,
                    ProductCategoryId=s.ProductCategory.Id,
                }).FirstOrDefault();
            return Ok(result);
        }

        // PUT: odata/Products(5)
        public IHttpActionResult Put([FromODataUri] int key,ProductViewModel model)
        {
            Product product = new Product
            {
                Id = model.Id,
                ProductCode = model.ProductCode,
                CategoryId = model.ProductCategoryId,
                Price = model.Price,
                Name = model.Name,
                Description = model.Description,
            };
            try
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: odata/Products
        public IHttpActionResult Post(ProductViewModel model)
        {
            var product = new Product
            {
                CategoryId = model.ProductCategoryId,
                Name = model.Name,
                Price = model.Price,
                ProductCode = model.ProductCode,
                Description = model.Description,
            };
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE: odata/Products(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var product = db.Products.Find(key);
            try
            {
                db.Products.Remove(product);
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
