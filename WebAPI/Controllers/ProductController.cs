using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        WebAPIContext db = new WebAPIContext();
        //Get: api/Product/GetProducts
        public IHttpActionResult GetProducts()
        {
            var result = db.Products.ToList().Select(x => new ProductViewModel
            {
                Name = x.Name,
                Category = x.Category,
                Price = x.Price

            });
            return Ok(new { data = result });
        }
        //GET: api/Product/GetProduct/Id
        //public ProductViewModel GetProduct(int Id)
        //{
        //    var product = db.Products.Where(i => i.Id == Id).Select(x => new ProductViewModel
        //    {
        //        Name = x.Name,
        //        Category = x.Category,
        //        Price = x.Price,
        //    }).SingleOrDefault();
        //    return product;

        //}


    }
}
