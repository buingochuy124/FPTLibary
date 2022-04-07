using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IUserDAO
    {
        int User_Login(string UserAccount, string UserPassword);

        List<UserDTO> Users_GetList();

    }
}
