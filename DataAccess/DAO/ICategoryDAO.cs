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
        CategoryDTO Categories_GetDetail(int CategoryID);

        int Categories_CategoryEdit(int CategoryID, string CategoryName);



    }
    
}
