using DataAccess.DTO;
using FPTLibary.Models;
using System.Linq;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class BookController : Controller
    {
        // GET: Library
        public ActionResult Index()
        {
            var userSession = Session[DataAccess.Libs.Config.SessionAccount];
            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("login", "Unauthenticate");
                }
                else
                {
                    var result = new DataAccess.DAOImpl.BookDAOImpl().Books_GetList();
                    foreach (var item in result)
                    {
                        item.CategoryName = new DataAccess.DAOImpl.CategoryDAOImpl()
                            .Categories_GetList()
                            .FirstOrDefault(c => c.CategoryID == item.CategoryID)
                            .CategoryName;

                    }
                    return View(result);
                }
                

            }
            catch (System.Exception)
            {
                throw;
            }

        }
        public ActionResult BookCreate()
        {
            var result = new DataAccess.DAOImpl.CategoryDAOImpl().Categories_GetList();


            return View(result);
        }
        
        public JsonResult BookInsert(string BookName, float Cost, int Quantity, string CategoryName, byte[] BookImage)
        {
            ReturnData returnData = new ReturnData();




            return Json(returnData, JsonRequestBehavior.AllowGet);

        }


    }
}