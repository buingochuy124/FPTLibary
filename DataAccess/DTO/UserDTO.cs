using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public string UserAdress { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
