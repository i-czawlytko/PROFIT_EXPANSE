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
        private IAuthHandler _auth;
        private IDBInit _dbInit;
        private DataHandlerFactory _dataHandlerFactory;
        public UserInfo userInfo { get; }

        public DataModel(IAuthHandler auth, IDBInit dbInit, DataHandlerFactory dataHandlerFactory)
        {
            _dbInit = dbInit;
            _dbInit.Init();

            _auth = auth;

            _dataHandlerFactory = dataHandlerFactory;
            userInfo = _auth.GetUserInfo();
            dataHandler = dataHandlerFactory.CreateDataHandler(userInfo.RoleType);
        }
    }
}
