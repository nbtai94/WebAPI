using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        public ActionResult ListCategories()
        {
            return View();
        }
        public ActionResult CategoryForm() {
            return View();
        }
    }
}