using System.Web.Mvc;
using WebAPI.Models;
using System.Collections;
using System.Linq;
namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        WebAPIContext db = new WebAPIContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Print(int id)// truyen len ma don hang
        {
            Order model = db.Orders.Where(i => i.Id == id).FirstOrDefault();
            return View(model);
        }
    }
}