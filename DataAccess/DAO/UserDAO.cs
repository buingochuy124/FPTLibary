using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public interface UserDAO
    {
        int User_Login(string UserAccount, string UserPassword);

    }
}
