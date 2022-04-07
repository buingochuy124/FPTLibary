using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
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
                    var result = new DataAccess.DAOImpl.CategoryDAOImpl().Categories_GetList();
                    return View(result);

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult CategoryGetListBook(int CategoryID)
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
                    var listBook = new DataAccess.DAOImpl.BookDAOImpl().Books_GetList();
                    var result = new List<DataAccess.DTO.BookDTO>();

                    foreach (var item in listBook)
                    {
                        if (item.CategoryID == CategoryID)
                        {
                            result.Add(item);
                        }
                    }

                    foreach (var item in result)
                    {
                        var categoryName = new DataAccess.DAOImpl
                            .CategoryDAOImpl()
                            .Categories_GetList()
                            .FirstOrDefault(c => c.CategoryID == item.CategoryID)
                            .CategoryName;

                        item.CategoryName = categoryName;

                    }


                    return View(result);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}