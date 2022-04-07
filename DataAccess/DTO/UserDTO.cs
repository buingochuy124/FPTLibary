using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        [EmailAddress]
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public string UserAdress { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
