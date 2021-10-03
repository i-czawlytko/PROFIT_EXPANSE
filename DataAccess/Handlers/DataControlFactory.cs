using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace DataAccess.Handlers
{
    public static class ControlHandlerFactory
    {
        public static BaseControlHandler CreateControlHandler(RoleType role)
        {
            switch (role)
            {
                case RoleType.Admin:
                    return new AdminControlHandler();
                case RoleType.User:
                    return new UserControlHandler();
                default:
                    throw new SecurityException();
            }
        }
    }
}
