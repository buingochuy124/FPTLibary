using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PopupPartial(string msg)
        {
            ViewBag.msg = msg;
            return PartialView();
        }
    }
}