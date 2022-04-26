using FPTLibary.Models;
using System;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult Index()
        {
            var result = new DataAccess.DAOImpl.SellerDAOImpl().Sellers_GetList();
            return View(result);
        }

        public ActionResult CreateSeller()
        {

            var dt = DateTime.Now;

            var result = new DataAccess.DTO.SellerDTO();
            result.SaleDate = dt;

            
            return View(result);
        }

        public JsonResult InsertSeller(string SellerName ,DateTime DateTime)
        {


            var returnData = new ReturnData();

            try
            {

                var result = new DataAccess.DAOImpl.SellerDAOImpl().Seller_Create(SellerName, DateTime);

                if (result > 0)
                {
                    returnData.ResponseCode = 999;
                    returnData.Description = "Create Seller Succesfully !!!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    returnData.ResponseCode = 999;
                    returnData.Description = "Create Seller Fail !!!";
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