using DataAccess.DTO;
using FPTLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class UserInventController : Controller
    {
        public ActionResult Index()
        {
            var userAccount = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            try
            {
                if (userAccount == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }

               

                var userRoles = new DataAccess.DAOImpl.UserRoleDAOImpl()
                    .GetUserRoleByUserID(userAccount.UserId);

                foreach (var item in userRoles)
                {
                    if (item.RoleID == 2)
                    {
                        ViewBag.userRoleId = 2;
                        return View();
                    }

                }
                return RedirectToAction("UserInvent", "UserInvent");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult ListUsersPartialView()
        {
            var result = new List<DataAccess.DTO.UserDTO>();
            try
            {
                result = new DataAccess.DAOImpl.UserDAOImpl().Users_GetList();
                return PartialView(result);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult UserInventPartialView()
        {
            var returnData = new ReturnData();
            var listUserInvent = new List<UserInventDTO>();
            var userAccount = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            var userID = userAccount.UserId;
            var result = new List<DataAccess.DTO.BookDTO>();
            try
            {
                if (userAccount.UserId != userID)
                {
                    returnData.ResponseCode = -998;
                    returnData.Description = "You not have permission to see other book invent!!!";
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    listUserInvent = new DataAccess.DAOImpl.UserInventDAOImpl()
                    .UserInvent_GetDetail(userID);

                    foreach (var item in listUserInvent)
                    {
                        result.Add(new DataAccess.DAOImpl.BookDAOImpl()
                            .Book_GetDetail(item.BookID));
                    }
                    return PartialView(result);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult AddBookToUserInvent(int? BookID)
        {
            try
            {

                var result = new DataAccess.DAOImpl.BookDAOImpl()
              .Book_GetDetail(BookID);

                var returnData = new ReturnData();


                var bookCategory = new DataAccess.DAOImpl.CategoryDAOImpl().Categories_GetList()
                    .FirstOrDefault(c => c.CategoryID == result.CategoryID).CategoryName;

                result.CategoryName = bookCategory;
            }
            catch (Exception)
            {

                throw;
            }
          




            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}