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
using WebAPI.Helper;

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
                    ProductCategoryId=s.ProductCategory.Id,
                    ProductCategoryName = s.ProductCategory.CategoryName,
                    Price = s.Price,
                    Description = s.Description,
                    NormalizeName = s.NormalizeName
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
                    ProductCategoryName = s.ProductCategory.CategoryName,
                    Name = s.Name,
                    Price = s.Price,
                    ProductCode = s.ProductCode,
                    Description = s.Description,
                    ProductCategoryId = s.ProductCategory.Id,
                }).FirstOrDefault();
            return Ok(result);
        }

        // PUT: odata/Products(5)
        public IHttpActionResult Put([FromODataUri] int key, ProductViewModel model)
        {
            Product product = db.Products.Where(i => i.Id == key).FirstOrDefault();
            product.Id = model.Id;
            product.CategoryId = model.ProductCategoryId;
            product.Price = model.Price;
            product.Name = model.Name;
            product.Description = model.Description;
            product.NormalizeName = Helper.Helper.ConvertToNormalize(model.Name);
            if (string.IsNullOrEmpty(model.ProductCode))
            {
                product.ProductCode = Helper.Helper.GenerateCode(DateTime.Now, 2);
            }
            else
            {
                var exist = db.Products.Where(i => i.ProductCode == model.ProductCode).FirstOrDefault();
                if (exist == null)
                {
                    product.ProductCode = model.ProductCode;
                }
                else
                {
                    if (product.Id == exist.Id)
                    {
                        product.ProductCode = model.ProductCode;
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }

            try
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        // POST: odata/Products
        public IHttpActionResult Post(ProductViewModel model)
        {

            Product product = new Product
            {
                CategoryId = model.ProductCategoryId,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                CreateDate = DateTime.Now,
                NormalizeName = Helper.Helper.ConvertToNormalize(model.Name)
            };
            if (string.IsNullOrEmpty(model.ProductCode))
            {
                product.ProductCode = Helper.Helper.GenerateCode(DateTime.Now, 2);
            }
            else
            {
                var exist = db.Products.Where(i => i.ProductCode == model.ProductCode).FirstOrDefault();
                if (exist == null)
                {
                    product.ProductCode = model.ProductCode;
                }
                else
                {
                    return BadRequest();
                }
            }
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
