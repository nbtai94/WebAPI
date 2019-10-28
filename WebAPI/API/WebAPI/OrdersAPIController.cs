using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace WebAPI.API
{
    public class OrdersAPIController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        //GET ORDERS
        [HttpGet]
        public IHttpActionResult Orders(int skip, int take)
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
                    OrderCode = s.OrderCode,
                    Note = s.Note,
                }); 
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
        public IHttpActionResult Orders(string key)
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
        public IHttpActionResult Orders(OrderViewModel model)
        {
            if (model != null)
            {
                if (model.Items.Count == 0 || model.CustomerId == 0)
                {
                    return BadRequest();
                }
                Order order = new Order
                {
                    CustomerId = model.CustomerId,
                    DateOrder = model.DateOrder,
                    DateCreated = DateTime.Now,
                    TotalMoney = model.TotalMoney,
                    Items = new List<OrderDetail>(),
                    OrderCode = "DH" + model.DateOrder.ToString()
                };
                foreach (var item in model.Items)
                {
                    OrderDetail ord = new OrderDetail
                    {
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };
                    order.Items.Add(ord);
                }
                db.Orders.Add(order);
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet]
        public IHttpActionResult Orders(int Id)
        {
            Order order = db.Orders.Where(i => i.Id == Id).SingleOrDefault();
            OrderViewModel response = new OrderViewModel
            {
                Items = new List<OrderDetailViewModel>(),

                CustomerAddress = order.Customer.Address,
                CustomerName = order.Customer.Name,
                CustomerPhone = order.Customer.Phone,
                CustomerId = order.CustomerId,
                DateCreated = order.DateCreated,
                DateOrder = order.DateOrder,
                TotalMoney = order.TotalMoney,
                OrderCode = order.OrderCode
            };

            foreach (var item in order.Items)
            {
                OrderDetailViewModel ord = new OrderDetailViewModel
                {
                    Id = item.Id,
                    ProductId = item.Product.Id,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductName = item.Product.Name
                };
                response.Items.Add(ord);
            }
            return Ok(new { data = response });
        }
        [HttpPut]
        public IHttpActionResult Order(int Id, OrderViewModel model)
        {

            if (model != null)
            {
                if (model.Items.Count == 0 || model.CustomerId == 0)
                {
                    return BadRequest();
                }
                Order order = db.Orders.Where(i => i.Id == Id).SingleOrDefault();//Lay ra order
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


            return BadRequest();
        }
        //DELETE: Xóa đơn hàng
        [HttpDelete]
        public IHttpActionResult Delete(int Id)
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
