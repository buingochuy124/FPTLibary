using FPTLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class UnauthenticateController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Unauthenticate");
        }

        public JsonResult LoginCheck(string UserAccount, string UserPassword)
        {
            var returnData = new ReturnData();

            var result = new DataAccess.DAOImpl.UserDAOImpl().User_Login(UserAccount, UserPassword);

            try
            {
                if (result > 0)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Login successfully !!!";

                    Session[DataAccess.Libs.Config.SessionAccount] = new DataAccess.DAOImpl
                        .UserDAOImpl()
                        .Users_GetList()
                        .FirstOrDefault(u => u.UserAccount == UserAccount);



                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else if (result == -1)
                {
                    returnData.ResponseCode = -998;
                    returnData.Description = "User Account not exist. Login Fail !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    returnData.ResponseCode = -997;
                    returnData.Description = "User Account or Password was wrong. Login Fail !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
            }


            catch (System.Exception)
            {
                returnData.ResponseCode = -999;
                returnData.Description = "System Bussy. please  f5";
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        public JsonResult RegisterCheck(string UserAccount, string UserPassword, string UserFullName, string UserAddress, string UserPhoneNumber)
        {
            var returnData = new ReturnData();
            var result = new DataAccess.DAOImpl.UserDAOImpl().User_Register(UserAccount, UserPassword, UserFullName, UserAddress, UserPhoneNumber);
            try
            {
                if(result == -1)
                {
                    returnData.ResponseCode = -999;
                    returnData.Description = "UserAccount already exist !! ";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else if( result > 0)
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Register Successfully!!!";
                    var userID = new DataAccess.DAOImpl.UserDAOImpl().User_GetUserIDByUserAccount(UserAccount);
                    var userGetRole = new DataAccess.DAOImpl.UserRoleDAOImpl().UserGetDefaultRole(userID);
                    var login = new DataAccess.DAOImpl.UserDAOImpl().User_Login(UserAccount, UserPassword);
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "System bussy please F5!!!";
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