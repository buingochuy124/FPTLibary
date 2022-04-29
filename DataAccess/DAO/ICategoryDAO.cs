using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public interface ICategoryDAO
    {
        List<CategoryDTO> Categories_GetList();

        CategoryDTO Category_GetDetailByID(int CategoryID);

        CategoryDTO Category_GetDetailByName(string CategoryName);


        int Category_Insert(string CategoryName);


    }
}
