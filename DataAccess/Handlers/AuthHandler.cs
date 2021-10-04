using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Interfaces;
namespace DataAccess.Handlers
{
    public class AuthHandler : IAuthHandler
    {
        private ILogin _login;
        private IRoleHandler _roleHandler;
        public AuthHandler(ILogin login, IRoleHandler roleHandler)
        {
            _login = login;
            _roleHandler = roleHandler;
        }
        public UserInfo GetUserInfo()
        {
            string userLogin = _login.GetLogin();

            if (userLogin == null) return new UserInfo { FullName = "_noname", RoleName = "_norole", RoleType = RoleType.Undefinded };

            return _roleHandler.GetUserInfo(userLogin);
        }
    }
}
