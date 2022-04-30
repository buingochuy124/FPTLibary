using DataAccess.DTO;
using FPTLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult Index()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            var result = new List<DataAccess.DTO.SaleDTO>();
            var userRole = new DataAccess.DAOImpl.RoleDAOImpl().Roles_GetList();
            try
            {
                if (userRole.Where(u => u.RoleID == userSession.UserId)
                    .FirstOrDefault(u => u.RoleID == 1) != null)
                {
                    return RedirectToAction("ListSellerPartialView", "Seller");
                }
                else
                {
                    result = new DataAccess.DAOImpl.SaleDAOImpl().Sales_GetListSaleByUserID(userSession.UserId);
                }

                return View(result);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult ListSellerPartialView()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            var userRole = new DataAccess.DAOImpl.RoleDAOImpl().Roles_GetList();

            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                    if (userRole.Where(u => u.RoleID == userSession.UserId)
                                       .FirstOrDefault(u => u.RoleID == 1) != null)
                    {
                        var result = new DataAccess.DAOImpl.SellerDAOImpl().Sellers_GetList();
                        return PartialView(result);

                    }

                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult CreateSeller()
        {

            //var dt = DateTime.Now;

            //var result = new DataAccess.DTO.SellerDTO();
            //result.SaleDate = dt;


            return View();
        }

        public JsonResult InsertSeller(string SellerName, DateTime DateTime)
        {


            //var returnData = new ReturnData();

            //try
            //{

            //    var result = new DataAccess.DAOImpl.SellerDAOImpl().Seller_Create(SellerName, DateTime);

            //    if (result > 0)
            //    {
            //        returnData.ResponseCode = 999;
            //        returnData.Description = "Create Seller Succesfully !!!";
            //        return Json(returnData, JsonRequestBehavior.AllowGet);

            //    }
            //    else
            //    {
            //        returnData.ResponseCode = 999;
            //        returnData.Description = "Create Seller Fail !!!";
            //        return Json(returnData, JsonRequestBehavior.AllowGet);
            //    }

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            return Json(JsonRequestBehavior.AllowGet);

        }
    }
}