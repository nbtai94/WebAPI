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
using WebAPI.Helper;
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
                    NormalizeCategoryName = s.NormalizeCategoryName
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
                    Id = s.Id,
                    CategoryName = s.CategoryName,
                    CategoryCode = s.CategoryCode,
                    CreateDate = s.CreateDate,
                }).FirstOrDefault();
            return Ok(result);
        }

        // PUT: odata/ProductCategories(5)
        public IHttpActionResult Put([FromODataUri] int key, ProductCategoryViewModel model)
        {
            ProductCategory category = db.ProductCategories.Where(i => i.Id == key).FirstOrDefault();

            category.Id = model.Id;
            category.CategoryCode = model.CategoryCode;
            category.CategoryName = model.CategoryName;
            category.CreateDate = model.CreateDate;
            category.NormalizeCategoryName = Helper.Helper.ConvertToNormalize(model.CategoryName);
            if (string.IsNullOrEmpty(model.CategoryCode))
            {
                category.CategoryCode = Helper.Helper.GenerateCode(DateTime.Now, 2);
                var exist = db.ProductCategories.Where(i => i.CategoryCode == category.CategoryCode).FirstOrDefault();
                if (exist != null)
                {
                    category.CategoryCode = Helper.Helper.GenerateCode(DateTime.Now, 2);
                }
            }
            else
            {
                var exist = db.Customers.Where(i => i.CustomerCode == model.CategoryCode).FirstOrDefault();
                if (exist == null)
                {
                    category.CategoryCode = model.CategoryCode;
                }
                else
                {
                    if (category.Id == exist.Id)
                    {
                        category.CategoryCode = model.CategoryCode;
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = new ProductCategory()
            {
                Id = model.Id,
                CategoryName = model.CategoryName,
                CreateDate = DateTime.Now,
                NormalizeCategoryName = Helper.Helper.ConvertToNormalize(model.CategoryName)
            };
            if (string.IsNullOrEmpty(model.CategoryCode))
            {
                category.CategoryCode = Helper.Helper.GenerateCode(DateTime.Now,2);
                if (db.ProductCategories.Where(i => i.CategoryCode == category.CategoryCode).FirstOrDefault() != null)
                {
                    category.CategoryCode = Helper.Helper.GenerateCode(DateTime.Now, 2);
                }
            }
            else
            {
                var exist = db.Customers.Where(i => i.CustomerCode == model.CategoryCode).FirstOrDefault();
                if (exist == null)
                {
                    category.CategoryCode = model.CategoryCode;
                }
                else
                {
                    return BadRequest();
                }
            }
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