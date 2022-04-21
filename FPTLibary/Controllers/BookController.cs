using DataAccess.DTO;
using FPTLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class BookController : Controller
    {

        public ActionResult Index()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            var listRoleOfUser = new List<UserRoleDTO>();
            var result = new RoleDTO();
            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("login", "Unauthenticate");
                }
                else
                {

                    var userRoles = new DataAccess.DAOImpl.UserRoleDAOImpl().UserRoles_GetList();

                    foreach (var item in userRoles)
                    {
                        if (item.UserID == userSession.UserId)
                        {
                            listRoleOfUser.Add(item);
                        }
                    }

                    foreach (var item in listRoleOfUser)
                    {

                        if (item.RoleID == 1)
                        {
                            result.RoleID = 1;
                            result.RoleName = new DataAccess.DAOImpl.RoleDAOImpl()
                                .Roles_GetList().FirstOrDefault(r => r.RoleID == result.RoleID)
                                .RoleName;
                        }
                        else
                        {

                            result.RoleID = 2;
                            result.RoleName = new DataAccess.DAOImpl.RoleDAOImpl()
                                .Roles_GetList().FirstOrDefault(r => r.RoleID == result.RoleID).RoleName;

                        }
                    }
                    ViewData["UserName"] = userSession.UserFullName;

                    return View(result);
                }


            }
            catch (System.Exception)
            {
                throw;
            }

        }
        public ActionResult BookLibraryPartialView(int? PageNumber, int? NumberPerPage)
        {
            if (PageNumber == null && NumberPerPage == null)
            {
                PageNumber = 1;
                NumberPerPage = 5;
            }
            var result = new DataAccess.DAOImpl.BookDAOImpl().Books_GetListByPage(PageNumber, NumberPerPage);


            ViewBag.CurrentPage = PageNumber;
            ViewBag.NumberPerPage = NumberPerPage;
            ViewBag.EndPage = (new DataAccess.DAOImpl.BookDAOImpl().Books_GetList().Count) / NumberPerPage +  1;

            foreach (var item in result)
            {
                item.CategoryName = new DataAccess.DAOImpl.CategoryDAOImpl()
                    .Categories_GetList()
                    .FirstOrDefault(c => c.CategoryID == item.CategoryID)
                    .CategoryName;
            }


            return PartialView(result);
            }
        public ActionResult BookCreate()
        {
            var result = new DataAccess.DAOImpl.CategoryDAOImpl().Categories_GetList();




            return View(result);
        }

        public JsonResult BookInsert(string BookName, float Cost, int Quantity, string CategoryName)
        {
            ReturnData returnData = new ReturnData();
            var categoryID = 0;
            var result = 0;

            try
            {


                categoryID = new DataAccess.DAOImpl.CategoryDAOImpl()
                .Categories_GetList()
                .FirstOrDefault(C => C.CategoryName == CategoryName)
                .CategoryID;

                result = new DataAccess.DAOImpl.BookDAOImpl()
                .Book_Create(BookName, Cost, Quantity, categoryID);

                if (result > 0)
                {
                    returnData.ResponseCode = 999;
                    returnData.Description = "Add Book Successfully !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = -999;
                    returnData.Description = "Add Book fail !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }


        }
        public ActionResult BookDetail(int? BookID)
        {

            try
            {
                var result = new DataAccess.DAOImpl
                .BookDAOImpl()
                .Books_GetList()
                .FirstOrDefault(b => b.BookID.Equals(BookID));


                result.CategoryName = new DataAccess.DAOImpl
                    .CategoryDAOImpl()
                    .Categories_GetList()
                    .FirstOrDefault(c => c.CategoryID == result.CategoryID)
                    .CategoryName;


                return View(result);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult BookUploadImage()
        {
            return View();
        }
    }
}