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
                    if (item.RoleID == 1)
                    {
                        ViewBag.userRoleId = 1;
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
        public JsonResult AddBookToUserInvent(int BookID)
        {
            var returnData = new ReturnData();
            var userAccount = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            var userID = userAccount.UserId;
            try
            {
                var result = new DataAccess.DAOImpl.UserInventDAOImpl()
                    .UserInvent_AddBook(BookID, userID);

                if (result > 0)
                {
                    returnData.ResponseCode = 99;
                    returnData.Description = "Add to invent successfully !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }
                else if(result == 0)
                {
                    returnData.ResponseCode = 0;
                    returnData.Description = "You already have this book in your inventory !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                    //return 
                }
                else
                {
                    returnData.ResponseCode = 0;
                    returnData.Description = "Some thing went wrong please try again !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}