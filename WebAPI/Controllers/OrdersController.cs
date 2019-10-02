using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public IHttpActionResult GetOrders(int skip, int take)
        {
            var total = db.Orders.Count();
            var result = db.Orders.OrderBy(x => x.Id)
                .Skip(skip)
                .Take(take)
                .Select(s => new OrderViewModel
                {
                    Id = s.Id,
                    Customer = s.Customer,
                    CustomerId = s.CustomerId,
                    DateCreated = s.DateCreated,
                    DateOrder = s.DateOrder,
                    TotalMoney = s.TotalMoney,
                    TotalQuantity = s.TotalQuantity,
                    Note = s.Note,
                    OrderDetails = s.OrderDetails

                });
            return Ok(new
            {
                data = result,
                total = total,
                skip = skip,
                take = take
            });
        }

        public IHttpActionResult AddOrder()
        {
            return Ok();
        }
    }
}