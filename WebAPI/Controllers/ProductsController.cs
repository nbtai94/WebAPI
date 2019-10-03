using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly WebAPIContext db = new WebAPIContext();

        //GET: api/Products
        public IHttpActionResult GetProducts(int skip, int take)
        {
            int total = db.Products.Count();
            var result = db.Products.OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take)
                .Select(s => new ProductViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    Category = s.Category
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
        public IHttpActionResult GetAllProducts()
        {
            var result = db.Products.OrderBy(x => x.Id)
                .Select(s => new ProductViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    Category = s.Category
                });

            return Ok(new
            {
                data = result,
            });
        }

        //GET: api/Product/SearchProduct
        [HttpGet]
        public IHttpActionResult SearchProduct(/*SearchViewModel model*/ string k)
        {
            var result = db.Products.OrderBy(x => x.Id).Where(x => x.Name.Contains(k) || x.Category.Contains(k) || k == null)
                .Select(s => new ProductViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    Category = s.Category
                });
            var total = result.Count();
            return Ok(new
            {
                data = result,
                total = total,
            });
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int key)
        {
            Product product = await db.Products.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}