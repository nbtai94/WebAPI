using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();
        //GET: Get all order
        public IHttpActionResult GetOrders(int skip, int take)
        {
            var total = db.Orders.Count();
            var result = db.Orders.OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take)
                .Select(s => new OrderViewModel
                {
                    Id = s.Id,


                    CustomerId = s.Customer.Id,
                    CustomerName = s.Customer.Name,
                    CustomerAddress = s.Customer.Address,
                    CustomerPhone = s.Customer.Phone,
                    DateCreated = s.DateCreated,
                    DateOrder = s.DateOrder,
                    TotalMoney = s.TotalMoney,
                    Note = s.Note,
                }); ;
            return Ok(new
            {
                data = result,
                total = total,
                skip = skip,
                take = take
            });
        }
        //GET: Tìm kiếm
        [HttpGet]
        public IHttpActionResult SearchOrder(string key)
        {
            var result = db.Orders.OrderBy(x => x.Id).Where(x => x.Customer.Name.Contains(key) || x.Customer.Address.Contains(key) || x.Customer.Phone.Contains(key) || key == null)
                .Select(s => new OrderViewModel
                {
                    Id = s.Id,
                    CustomerId = s.Customer.Id,
                    CustomerName = s.Customer.Name,
                    CustomerAddress = s.Customer.Address,
                    CustomerPhone = s.Customer.Phone,
                    DateCreated = s.DateCreated,
                    DateOrder = s.DateOrder,
                    TotalMoney = s.TotalMoney,
                    Note = s.Note,

                });
            var total = result.Count();
            return Ok(new
            {
                data = result,
                total = total,
            });
        }

        //POST: Tạo đơn hàng
        [HttpPost]
        public IHttpActionResult AddOrder(OrderViewModel model)
        {
            if (model != null)
            {
                Order order = new Order();
                order.Items = new List<OrderDetail>();
                order.CustomerId = model.CustomerId;
                order.DateOrder = model.DateOrder;
                order.DateCreated = DateTime.Now;
                order.TotalMoney = model.TotalMoney;


                foreach (var item in model.Items)
                {
                    OrderDetail ord = new OrderDetail();
                    ord.ProductId = item.Id;
                    ord.Price = item.Price;
                    ord.Quantity = item.Quantity;
                    order.Items.Add(ord);
                }
                db.Orders.Add(order);
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        //GET: GET 1 ORDER
        [HttpGet]
        public IHttpActionResult GetOrderDetail(int Id)
        {
            OrderViewModel response = new OrderViewModel();
            response.Items = new List<OrderDetailViewModel>();


            Order order = db.Orders.Where(i => i.Id == Id).SingleOrDefault();

            response.CustomerAddress = order.Customer.Address;
            response.CustomerName = order.Customer.Name;
            response.CustomerPhone = order.Customer.Phone;
            response.CustomerId = order.CustomerId;
            response.DateCreated = order.DateCreated;
            response.DateOrder = order.DateOrder;
            response.TotalMoney = order.TotalMoney;


            foreach (var item in order.Items)
            {
                OrderDetailViewModel ord = new OrderDetailViewModel();
                ord.Id = item.Product.Id;
                ord.Price = item.Price;
                ord.Quantity = item.Quantity;
                ord.ProductName = item.Product.Name;
                response.Items.Add(ord);
            }

            return Ok(new { data = response });
        }
        //PUT: Sửa đơn hàng
        [HttpPut]
        public IHttpActionResult EditOrder(int Id, OrderViewModel model)
        {

            if (model != null)
            {
                Order order = db.Orders.Where(i => i.Id == Id).SingleOrDefault();//Lay ra order
                order.Items = db.OrderDetails.Where(i => i.OrderId == model.Id).ToList();//Lay ra list chi tiet order cũ
                order.DateOrder = model.DateOrder;
                order.DateCreated = DateTime.Now;
                order.TotalMoney = model.TotalMoney;
                order.CustomerId = model.CustomerId;
                var ids = model.Items.Select(s => s.Id).ToList();//list item mới truyền lên
                var itemRemove = order.Items.Where(x => !ids.Contains(x.Id)).ToList();//list item cần xóa

                foreach (var i in itemRemove)
                {
                    db.OrderDetails.Remove(i);

                }
                //foreach (var item in model.Items)
                //{
                //}

                foreach (var item in model.Items)
                {
                    var o = order.Items.Where(i => i.ProductId == item.Id).FirstOrDefault();
                    if (o != null)
                    {
                        o.Quantity = item.Quantity;
                        o.Price = item.Price;
                    }
                    else
                    {
                        var ord = new OrderDetail();
                        ord.ProductId = item.Id;
                        ord.Quantity = item.Quantity;
                        ord.Price = item.Price;
                        order.Items.Add(ord);
                    }
                }
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        //DELETE: Xóa đơn hàng
        [HttpDelete]
        public IHttpActionResult RemoveOrder(int Id)
        {
            var result = db.Orders.Where(i => i.Id == Id).SingleOrDefault();
            if (result != null)
            {
                db.Orders.Remove(result);
                db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}