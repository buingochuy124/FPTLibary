using DataAccess.DTO;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            try
            {
                var accountLogin = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount] != null ? 
                    (UserDTO)Session[DataAccess.Libs.Config.SessionAccount] : new UserDTO(); 

                if (accountLogin.UserId <= 0)
                {
                    return RedirectToAction("login", "Unauthenticate");
                }
                else
                {
                    return View();
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }


    }
}