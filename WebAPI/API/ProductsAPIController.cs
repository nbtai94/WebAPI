using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace WebAPI.API
{
    public class ProductsAPIController : ApiController
    {
        WebAPIContext db = new WebAPIContext();
        [HttpGet]
        public IHttpActionResult Products(int skip, int take)
        {
            var result = db.Products.OrderBy(i => i.Id)
                .Skip(skip)
                .Take(take)
                .Select(s => new ProductViewModel
                {
                    Id = s.Id,
                    ProductCode = s.ProductCode,
                    ProductCategoryName = s.ProductCategory.CategoryName,
                    Name = s.Name,
                    Price = s.Price,
                });
            int total = db.Products.Count();
            return Ok(new
            {
                data = result,
                total = total
            });
        }
        [HttpGet]
        public IHttpActionResult Products()
        {

            var result = db.Products.Select(s => new 
            {
                Id = s.Id,
                Name = s.Name,
                ProductCategoryName = s.ProductCategory.CategoryName,
                Price = s.Price,
                ProductCode = s.ProductCode,
            });
            return Ok(new { data = result });
        }
        [HttpGet]
        public IHttpActionResult SearchProduct(/*SearchViewModel model*/ string k)
        {
            var result = db.Products.OrderBy(x => x.Id).Where(x => x.Name.Contains(k) || x.ProductCode.Contains(k) || x.ProductCategory.CategoryName.Contains(k) || string.IsNullOrEmpty(k))

                .Select(s => new ProductViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    ProductCategoryName = s.ProductCategory.CategoryName,
                    ProductCode = s.ProductCode
                });
            var total = result.Count();
            return Ok(new
            {
                data = result,
                total = total,
            });
        }
        [HttpGet]
        public IHttpActionResult Products(int id)
        {
            var result = db.Products.Where(i => i.Id == id).SingleOrDefault();
            var res = new ProductViewModel
            {
                Id = result.Id,
                ProductCode = result.ProductCode,
                ProductCategoryId = result.ProductCategory.Id,
                ProductCategoryName = result.ProductCategory.CategoryName,
                Name = result.Name,
                Price = result.Price,
                Description = result.Description

            };
            return Ok(res);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int key)
        {
            var product = db.Products.Find(key);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var orderdetails = db.OrderDetails.Where(i => i.ProductId == key).ToList();
                if (orderdetails.Count == 0)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }


        }
        [HttpPost]
        public IHttpActionResult Products(ProductViewModel model)
        {
            if (model != null)
            {
                var product = new Product
                {
                    CategoryId = model.ProductCategoryId,
                    Name = model.Name,
                    Price = model.Price,
                    ProductCode = model.ProductCode,
                    Description = model.Description,
                };
                db.Products.Add(product);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        public IHttpActionResult Product(ProductViewModel model)
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
                throw;
            }
            return Ok();
        }
    }
}
