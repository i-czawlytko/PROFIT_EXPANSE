using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Interfaces;
namespace DataAccess.Handlers
{
    public class AuthHandler : IAuthHandler
    {
        private ILogin login;
        private IRoleHandler roleHandler;
        public AuthHandler()
        {
            login = new WindowsAuthLogin();
            roleHandler = new MsSqlRoleHandler();
        }
        public UserInfo GetUserInfo()
        {
            string userLogin = login.GetLogin();

            if (userLogin == null) return new UserInfo { FullName = "_noname", RoleName = "_norole", RoleType = RoleType.Undefinded };

            return roleHandler.GetUserInfo(userLogin);
        }
    }
}
