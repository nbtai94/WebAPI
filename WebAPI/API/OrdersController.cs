using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();
        #region GET ORDERS
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
        #endregion
        #region GET SEARCH
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
        #endregion
        #region POST ORDER
        //POST: Tạo đơn hàng
        [HttpPost]
        public IHttpActionResult AddOrder(OrderViewModel model)
        {
            if (model != null)
            {
                if (model.Items.Count == 0 || model.DateOrder == null || model.CustomerId == 0)
                {
                    return NotFound();
                }
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
        #endregion
        #region GET ORDER
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
                ord.Id = item.Id;
                ord.ProductId = item.Product.Id;
                ord.Price = item.Price;
                ord.Quantity = item.Quantity;
                ord.ProductName = item.Product.Name;
                response.Items.Add(ord);
            }
            return Ok(new { data = response });
        }
        #endregion
        #region EDIT ORDER
        //PUT: Sửa đơn hàng
        [HttpPut]
        public IHttpActionResult EditOrder(int Id, OrderViewModel model)
        {

            if (model != null)
            {
                if (model.Items.Count == 0)
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
                        OrderDetail ord = new OrderDetail();
                        ord.ProductId = item.Id;
                        ord.Price = item.Price;
                        ord.Quantity = item.Quantity;
                        order.Items.Add(ord);
                    }
            
                }
                db.SaveChanges();
                return Ok();
            }


            return BadRequest();
        }
        #endregion
        #region DELETE ORDER
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
        #endregion
    }
}