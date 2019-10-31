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

namespace WebAPI.API.OdataAPI
{
    public class OrdersController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/Orders
        [EnableQuery]
        public IHttpActionResult Get(ODataQueryOptions<OrderViewModel> queryOptions)
        {
            try
            {
                var result = db.Orders.Select(s => new OrderViewModel
                {
                    Id = s.Id,
                    CustomerId = s.Customer.Id,
                    CustomerName = s.Customer.Name,
                    CustomerAddress = s.Customer.Address,
                    CustomerPhone = s.Customer.Phone,
                    DateCreated = s.DateCreated,
                    DateOrder = s.DateOrder,
                    TotalMoney = s.TotalMoney,
                    OrderCode = s.OrderCode,
                    Note = s.Note,
                });
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [EnableQuery]
        // GET: odata/Orders(5)
        public IHttpActionResult Get([FromODataUri] int key, ODataQueryOptions<OrderViewModel> queryOptions)
        {
            try
            {
                Order order = db.Orders.Where(i => i.Id == key).SingleOrDefault();//Lay order ra

                OrderViewModel result = new OrderViewModel
                {
                    CustomerId = order.Customer.Id,
                    CustomerAddress = order.Customer.Address,
                    CustomerName = order.Customer.Name,
                    CustomerPhone = order.Customer.Phone,
                    DateCreated = order.DateCreated,
                    OrderCode = order.OrderCode,
                    DateOrder = order.DateOrder,
                    TotalMoney = order.TotalMoney,
                    Note = order.Note,
                    Items = new List<OrderDetailViewModel>()
                };
                foreach (var item in order.Items)
                {
                    var detail = new OrderDetailViewModel
                    {

                        Id = item.Id,
                        ProductId = item.Product.Id,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        ProductName = item.Product.Name,
                    };
                    result.Items.Add(detail);
                }
                
                return Ok(result);
            }

            catch
            {
                return BadRequest();
            }

        }
        // PUT: odata/Orders(5)
        public IHttpActionResult Put([FromODataUri] int key, OrderViewModel model)
        {
            if (model.Items.Count == 0 || model.CustomerId == 0)
            {
                return BadRequest();
            }
            Order order = db.Orders.Where(i => i.Id == key).SingleOrDefault();//Lay ra order
            order.Items = db.OrderDetails.Where(i => i.OrderId == model.Id).ToList(); //Lay ra list chi tiet order cũ
            order.DateOrder = model.DateOrder;
            order.DateCreated = DateTime.Now;
            order.TotalMoney = model.TotalMoney;
            order.CustomerId = model.CustomerId;

            var ids = model.Items.Select(s => s.Id).ToList();//list item mới truyền lên
            List<OrderDetail> itemRemoves = order.Items.Where(x => !ids.Contains(x.Id)).ToList();
            foreach (var i in itemRemoves)
            {
                db.OrderDetails.Remove(i);
            }
            foreach (var item in model.Items)//duyệt list items mới truyền lên chưa có thì thêm vào db có rồi thì update propety
            {
                var orderDetail = db.OrderDetails.Where(i => i.Id == item.Id).FirstOrDefault();// lấy 1 chi tiết có id sản phẩm = id sản phẩm truyền lên

                if (orderDetail != null)
                {
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Price = item.Price;
                }
                else
                {
                    OrderDetail ord = new OrderDetail
                    {
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };
                    order.Items.Add(ord);
                }

            }
            db.SaveChanges();
            return Ok();
        }
        // POST: odata/Orders
        public IHttpActionResult Post(OrderViewModel model)
        {

            if (model.Items.Count == 0 || model.CustomerId == 0)
            {
                return BadRequest();
            }
            Order order = new Order
            {
                CustomerId = model.CustomerId,
                DateCreated = DateTime.Now,
                DateOrder = model.DateOrder,
                TotalMoney = model.TotalMoney,
                OrderCode = "DH" +  model.DateOrder.ToString(),
                Items = new List<OrderDetail>(),
            };
            foreach (var item in model.Items)
            {
                OrderDetail detai = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                order.Items.Add(detai);
            }
            try
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();

        }

        // DELETE: odata/Orders(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var order = db.Orders.Find(key);
            try
            {
                db.Orders.Remove(order);
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
