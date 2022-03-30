using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class LibraryController : Controller
    {
        // GET: Library
        public ActionResult Index()
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
}