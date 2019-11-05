using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class CodeManagerController : Controller
    {
        // GET: CodeManager
        public ActionResult CodeManager()
        {
            return View();
        }
        public ActionResult CodeManagerForm()
        {
            return View();
        }
    }
}