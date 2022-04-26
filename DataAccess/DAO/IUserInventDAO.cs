using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IUserInventDAO
    {
        List<UserInventDTO> UserInvents_GetList();

        List<UserInventDTO> UserInvent_GetDetail(int UserID);

        int UserInvent_AddBook(int BookID, int UserID);
    }
}
