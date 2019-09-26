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

        //Gett: api/Product/GetAllProduct
        [HttpGet]
        public IEnumerable<ProductViewModel> GetAllProduct()
        {


            var data = db.Products.ToList().OrderBy(x => x.Id);
            var result = data.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Price = x.Price,
            });
            return result.ToList();
        }

        //POST: api/Product/AddProduct
        [HttpPost]
        public HttpResponseMessage AddProduct(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product();
                    product.Id = model.Id;
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Category = model.Category;
                    db.Products.Add(product);
                    var result = db.SaveChanges();
                    if (result > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, "Thêm thành công!");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Đã xảy ra lỗi vui lòng thử lại !");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Đã xảy ra lỗi vui lòng thử lại !");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Đã xảy ra lỗi vui lòng thử lại !", ex);
            }
        }
        //DELETE api/Product/RemoveProduct/5
        [HttpDelete]

        public HttpResponseMessage RemoveProduct(int Id)
        {
            Product product = db.Products.Where(i => i.Id == Id).SingleOrDefault();

            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                 return Request.CreateResponse(HttpStatusCode.OK, "Đã xóa thành công!");
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
           
        }

        //////Get 1 product
        //[HttpGet]
        //public ProductViewModel GetProduct(int Id)
        //{
        //    var data = db.Products.Where(x => x.Id == Id).FirstOrDefault();
        //    if (data != null)
        //    {
        //        ProductViewModel product = new ProductViewModel();
        //        product.Id = data.Id;
        //        product.Name = data.Name;
        //        product.Category = data.Category;
        //        product.Price = data.Price;
        //        return product;
        //    }
        //    else
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }
        //}




    }
}
