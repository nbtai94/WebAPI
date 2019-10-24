using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using WebAPI.ViewModels;
using Microsoft.Data.OData;
using WebAPI.Models;

namespace WebAPI.API
{

    public class ProductsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        WebAPIContext db = new WebAPIContext();
        // GET: odata/Products
        [EnableQuery(PageSize = 10)]


        public IHttpActionResult Get(ODataQueryOptions<ProductViewModel> queryOptions)
        {
            return Ok(db.Products.Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ProductCode = s.ProductCode,
                ProductCategoryName = s.ProductCategory.CategoryName,
                Price = s.Price,
                Description = s.Description,
            }));
        }
        // GET: odata/Products(5)
        public SingleResult<Product> Get([FromODataUri] int key)
        {
            IQueryable<Product> result = db.Products.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }

        // PUT: odata/Products(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<ProductViewModel> delta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // TODO: Get the entity here.

            // delta.Put(productViewModel);

            // TODO: Save the patched entity.

            // return Updated(productViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Products
        public IHttpActionResult Post(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(productViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ProductViewModel> delta)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(productViewModel);

            // TODO: Save the patched entity.

            // return Updated(productViewModel);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Products(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
