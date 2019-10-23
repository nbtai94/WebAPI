using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Order
        public ActionResult ListOrder()
        {
            return View();
        }
        public ActionResult OrderForm()
        {
            return View();
        }
        public ActionResult OrderDetail()
        {
            return View();
        }
    }
}