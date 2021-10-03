using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    interface IRoleHandler
    {
        public UserInfo GetUserInfo(string userLogin);
    }
}
