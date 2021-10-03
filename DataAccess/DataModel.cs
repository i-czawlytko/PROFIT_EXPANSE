using DataAccess.Handlers;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class DataModel
    {
        public IDataHandler dataHandler { get; }
        private IAuthHandler auth;
        public UserInfo userInfo { get; }

        public DataModel()
        {
            DBInit.Init();
            IAuthHandler auth = new AuthHandler();
            userInfo = auth.GetUserInfo();
            dataHandler = DataHandlerFactory.CreateDataHandler(userInfo.RoleType);
        }
    }
}
