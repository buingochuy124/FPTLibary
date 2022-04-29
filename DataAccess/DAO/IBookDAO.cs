using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IBookDAO
    {
        List<BookDTO> Books_GetList();

        int Book_Create(string BookISBN, string BookName, string Author, float Cost, int Pages, int CategoryID, string Description, string BookImage);

        BookDTO Book_GetDetail(int? BookID);

        List<BookDTO> Books_GetListByPage(int? PageNumber, int? NumberPerPage);

    }
}
