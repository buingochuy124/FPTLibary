using System;

namespace DataAccess.DTO
{
    public class SaleDTO
    {
        public int SaleID { get; set; }
        public int SellerID { get; set; }
        public int BookID { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
