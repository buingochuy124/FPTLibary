using DataAccess.DTO;
using FPTLibary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FPTLibary.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            try
            {
                var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
                if (userSession.UserId <= 0)
                {
                    return RedirectToAction("login", "Unauthenticate");
                }
                else
                {
                    return View();
                }


            }
            catch (System.Exception)
            {

                throw;
            }

        }


        public ActionResult UserManagement()
        {
            var result = new List<DataAccess.DTO.UserDTO>();

            var userRole = new DataAccess.DTO.UserRoleDTO();

            var userRoleName = "";

            ReturnData returnData = new ReturnData();
            try
            {
                UserDTO accountLogin = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];

                if (accountLogin == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                 

                    userRole = new DataAccess.DAOImpl.UserRoleDAOImpl()
                        .UserRoles_GetList()
                        .FirstOrDefault(u => u.UserID == accountLogin.UserId);



                    userRoleName = new DataAccess.DAOImpl.RoleDAOImpl()
                        .Roles_GetList()
                        .FirstOrDefault(r => r.RoleID == userRole.RoleID)
                        .RoleName;



                    if (userRoleName == "Admin")
                    {
                        result = new DataAccess.DAOImpl.UserDAOImpl().Users_GetList();
                        return View(result);
                    }
                    else
                    {
                        returnData.Description = "You dont have permission !!!";
                        returnData.ResponseCode = -998;
                        return RedirectToAction("Index", "Home");
                    }
                    
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}