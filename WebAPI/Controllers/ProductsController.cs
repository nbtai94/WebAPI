//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Description;
//using System.Web.Mvc;
//using WebAPI.Models;
//using WebAPI.ViewModels;

//namespace WebAPI.Controllers
//{
//    public class ProductsController : ApiController
//    {
//        private WebAPIContext db = new WebAPIContext();


//        // GET: api/Products GetAllProduct
//        public IEnumerable<ProductViewModel> GetAllProducts()
//        {
//            var data = db.Products.ToList().OrderBy(o => o.Id);
//            var result = data.Select(x => new ProductViewModel()
//            {
//                Id = x.Id,
//                Name = x.Name,
//                Category = x.Category,
//                Price = x.Price
//            });
//            return result.ToList();
//        }

//        // GET: api/Products/5
//        [ResponseType(typeof(Product))]
//        public async Task<IHttpActionResult> GetProduct(int id)
//        {
//            Product product = await db.Products.FindAsync(id);
//            if (product == null)
//            {
//                return NotFound();
//            }
//            return Ok(product);
//        }

//        // PUT: api/Products/5
//        [ResponseType(typeof(void))]
//        public async Task<IHttpActionResult> PutProduct(int id, Product product)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != product.Id)
//            {
//                return BadRequest();
//            }

//            db.Entry(product).State = EntityState.Modified;

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ProductExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST: api/Products
//        //[ResponseType(typeof(Product))]
//        public async Task<IHttpActionResult> PostProduct(ProductViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var product = new Product
//            {
//                Name = model.Name,
//                Price = model.Price,
//                Category = model.Category

//            };
//            db.Products.Add(product);
//            await db.SaveChangesAsync();
//            model.Id = product.Id;
//            return Ok(model);
//        }

//        // DELETE: api/Products/5
//        //[ResponseType(typeof(Product))]
//        public async Task<IHttpActionResult> DeleteProduct(ProductViewModel model)
//        {
//            Product product = db.Products.Where(x => x.Id == model.Id).FirstOrDefault();
//            if (product == null)
//            {
//                return NotFound();
//            }

//            db.Products.Remove(product);
//            await db.SaveChangesAsync();

//            return Ok(product);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool ProductExists(int id)
//        {
//            return db.Products.Count(e => e.Id == id) > 0;
//        }
//    }
//}