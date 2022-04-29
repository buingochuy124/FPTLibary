using DataAccess.DTO;
using FPTLibary.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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


            try
            {
                if (PageNumber == null && NumberPerPage == null)
                {
                    PageNumber = 1;
                    NumberPerPage = 6;
                }
                var result = new DataAccess.DAOImpl.BookDAOImpl().Books_GetListByPage(PageNumber, NumberPerPage);
                ViewBag.CurrentPage = PageNumber;
                ViewBag.NumberPerPage = NumberPerPage;
                ViewBag.EndPage = (new DataAccess.DAOImpl.BookDAOImpl().Books_GetList().Count) / NumberPerPage + 1;
                if (PageNumber > ViewBag.EndPage)
                {
                    return HttpNotFound();
                }
                foreach (var item in result)
                {
                    item.CategoryName = new DataAccess.DAOImpl.CategoryDAOImpl()
                        .Category_GetDetailByID(item.CategoryID).CategoryName;
                }
                return PartialView(result);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult BookCreate()
        {
            var result = new DataAccess.DAOImpl.CategoryDAOImpl().Categories_GetList();
            return View(result);
        }

        //public JsonResult BookInsert(string BookName, float Cost, int Quantity, string CategoryName)
        //{
        //    ReturnData returnData = new ReturnData();
        //    var categoryID = 0;
        //    var result = 0;
        //    try
        //    {


        //        categoryID = new DataAccess.DAOImpl.CategoryDAOImpl()
        //        .Categories_GetList()
        //        .FirstOrDefault(C => C.CategoryName == CategoryName)
        //        .CategoryID;

        //        result = new DataAccess.DAOImpl.BookDAOImpl()
        //        .Book_Create(BookName, Cost, Quantity, categoryID);

        //        if (result > 0)
        //        {
        //            returnData.ResponseCode = 999;
        //            returnData.Description = "Add Book Successfully !!!";
        //            return Json(returnData, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            returnData.ResponseCode = -999;
        //            returnData.Description = "Add Book fail !!!";
        //            return Json(returnData, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }


        //}
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
                //result.SellerName = new DataAccess.DAOImpl.SellerDAOImpl()
                //    .Sellers_GetList()
                //    .FirstOrDefault(s => s.SellerID == result.SellerID)
                //    .SellerName;


                return View(result);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult BookEdit(int? BookID)
        {
            var result = new DataAccess.DAOImpl.BookDAOImpl().Book_GetDetail(BookID);
            return View(result);
        }
        
        public JsonResult ImportBookByExel()
        {
            var index_fail = "";
            var returnData = new ReturnData();
            try
            {
                HttpPostedFileBase excelFile = Request.Files["ExcelFile"];
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                if (excelFile == null)
                {
                    returnData.ResponseCode = -3;
                    returnData.Description = "File Empty";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }
                var package = new ExcelPackage(excelFile.InputStream);

                ExcelWorksheet ws = package.Workbook.Worksheets[0];

                for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                {

                    if (ws.Cells[rw, 1].Value != null
                        && ws.Cells[rw, 2].Value != null
                        && ws.Cells[rw, 3].Value != null
                        && ws.Cells[rw, 4].Value != null
                        && ws.Cells[rw, 5].Value != null
                        && ws.Cells[rw, 6].Value != null
                        && ws.Cells[rw, 7].Value != null
                        && ws.Cells[rw, 8].Value != null
                       )

                    {
                        //1
                        var bookISBN = ws.Cells[rw, 1].Value != null ? ws.Cells[rw, 1].Value.ToString() : string.Empty;
                        //2
                        var title = ws.Cells[rw, 2].Value != null ? ws.Cells[rw, 2].Value.ToString() : string.Empty;
                        //3
                        var author = ws.Cells[rw, 3].Value != null ? ws.Cells[rw, 3].Value.ToString() : string.Empty;
                        //4
                        var categoryName = ws.Cells[rw, 4].Value != null ? ws.Cells[rw, 4].Value.ToString() : string.Empty;
                        //5
                        var bookPages = ws.Cells[rw, 5].Value != null ? ws.Cells[rw, 5].Value.ToString() : string.Empty;
                        //6
                        var bookCost = ws.Cells[rw, 6].Value != null ? ws.Cells[rw, 6].Value.ToString() : string.Empty;
                        //7
                        var bookDescription = ws.Cells[rw, 7].Value != null ? ws.Cells[rw, 7].Value.ToString() : string.Empty;
                        //8
                        var bookImageURL = ws.Cells[rw, 8].Value != null ? ws.Cells[rw, 8].Value.ToString() : string.Empty;

                        var categoryID = new DataAccess.DAOImpl.CategoryDAOImpl().Category_GetDetailByName(categoryName).CategoryID;
                        var result =  new DataAccess.DAOImpl.BookDAOImpl().Book_Create(bookISBN, title, author, double.Parse(bookCost), int.Parse(bookPages), categoryID, bookDescription, bookImageURL);

                        var err_des = "";
                        try
                        {
                            if (result == 0)
                            {
                                err_des = "Book Already exist";

                            }
                            else if( result < 0)
                            {
                                err_des = "Add book fail";
                            }

                            else
                            {
                                index_fail = string.Empty; 
                            }

                            index_fail += err_des;


                        }
                        catch (System.Exception)
                        {

                            throw;
                        }


                    }
                }

                if (string.IsNullOrEmpty(index_fail))
                {
                    returnData.ResponseCode = 1;
                    returnData.Description = "Insert File book Sucessfully";
                    return Json(returnData,JsonRequestBehavior.AllowGet);

                }
                else
                {
                    returnData.ResponseCode = -1;
                    // returnData.Description = "System Bussy";
                    returnData.Description = index_fail;
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }


            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public void ExportExcel()
        {

            var model = new DataAccess.DAOImpl.BookDAOImpl().Books_GetList();

            try
            {
                foreach (var item in model)
                {
                    item.CategoryName = new DataAccess.DAOImpl.CategoryDAOImpl().Category_GetDetailByID(item.CategoryID).CategoryName;
                }
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;


                //Bước 2: 

                ExcelPackage ep = new ExcelPackage();
                ExcelWorksheet Sheet = ep.Workbook.Worksheets.Add("Report");
                Sheet.Cells["A1"].Value = "ID";
                Sheet.Cells["B1"].Value = "Book ISBN";
                Sheet.Cells["C1"].Value = "Title";
                Sheet.Cells["D1"].Value = "Author";
                Sheet.Cells["E1"].Value = "Category";
                Sheet.Cells["F1"].Value = "Pages";
                Sheet.Cells["G1"].Value = "Price";
                Sheet.Cells["H1"].Value = "Description";
                Sheet.Cells["I1"].Value = "Image Url";

                int row = 2;// dòng bắt đầu ghi dữ liệu
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        Sheet.Cells[string.Format("A{0}", row)].Value = item.BookID;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.BookISBN;
                        Sheet.Cells[string.Format("B{0}", row)].Value = item.BookName;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.Author;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.CategoryName;
                        Sheet.Cells[string.Format("E{0}", row)].Value = item.Pages;
                        Sheet.Cells[string.Format("D{0}", row)].Value = item.Cost;
                        Sheet.Cells[string.Format("G{0}", row)].Value = item.Description;
                        Sheet.Cells[string.Format("H{0}", row)].Value = item.BookImageURL;


                        row++;
                    }
                }
                Sheet.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + "Report.xlsx");
                Response.BinaryWrite(ep.GetAsByteArray());
                Response.End();


            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}