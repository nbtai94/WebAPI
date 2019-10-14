using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult ListCustomer()
        {
            return View();
        }
        public ActionResult CustomerForm()
        {
            return View();
        }
    }
}