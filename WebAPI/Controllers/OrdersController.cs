using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private WebAPIContext db = new WebAPIContext();

        public IHttpActionResult GetOrders(int skip, int take)
        {
            var total = db.Orders.Count();
            var result = db.Orders.OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take)
                .Select(s => new OrderViewModel
                {
                    Id = s.Id,
                    CustomerId = s.CustomerId,
                    DateCreated = s.DateCreated,
                    DateOrder = s.DateOrder,
                    TotalMoney = s.TotalMoney,
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

        public IHttpActionResult AddOrder(OrderViewModel model)
        {
            if (model != null)
            {
                Order order = new Order();
                order.DateOrder = model.DateOrder;
                order.DateCreated = DateTime.Now;
                order.TotalMoney = model.TotalMoney;
                order.CustomerId = model.CustomerId;
               

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
    }
}