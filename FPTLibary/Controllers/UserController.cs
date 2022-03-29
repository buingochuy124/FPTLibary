using DataAccess.DTO;
using System.Collections.Generic;
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
                var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
                if ( userSession.UserId <= 0)
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


        public ActionResult UserManagement()
        {
            var result = new List<DataAccess.DTO.UserDTO>();

            try
            {
                var accountLogin = Session[DataAccess.Libs.Config.SessionAccount];

                if(accountLogin == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }

                result = new DataAccess.DAOImpl.UserDAOImpl().Users_GetList();


                return View(result);


            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}