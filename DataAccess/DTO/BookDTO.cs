using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class BookDTO
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public float Cost { get; set; }
        public int Quantity { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
