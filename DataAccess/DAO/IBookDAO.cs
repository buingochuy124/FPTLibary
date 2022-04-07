using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface IBookDAO
    {
        List<BookDTO> Books_GetList();

        int Book_Create(string BookName, float Cost, int Quantity, int CategoryID, byte[] BookImage);

    }
}
