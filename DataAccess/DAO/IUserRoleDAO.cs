using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IUserRoleDAO
    {
        List<UserRoleDTO> UserRoles_GetList();

        List<UserRoleDTO> GetUserRoleByUserID(int UserID);

        int UserGetDefaultRole(int UserID);

    }
}
