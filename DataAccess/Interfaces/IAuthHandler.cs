using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IAuthHandler
    {
        public UserInfo GetUserInfo();
    }
}
