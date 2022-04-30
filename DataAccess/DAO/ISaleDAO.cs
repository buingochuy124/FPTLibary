using DataAccess.DTO;
using System.Collections.Generic;

namespace DataAccess.DAO
{
    public interface ISaleDAO
    {
        List<SaleDTO> Sales_GetList();

        List<SaleDTO> Sales_GetListSaleByUserID(int UserID);

    }
}
