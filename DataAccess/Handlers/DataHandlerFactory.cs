using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace DataAccess.Handlers
{
    public static class DataHandlerFactory
    {
        public static BaseDataHandler CreateDataHandler(RoleType role)
        {
            switch (role)
            {
                case RoleType.Admin:
                    return new AdminDataHandler();
                case RoleType.User:
                    return new UserDataHandler();
                default:
                    throw new SecurityException("Пользователь отсутствует в базе данных");
            }
        }
    }
}
