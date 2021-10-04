using DataAccess.Handlers;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ControlModel
    {
        public IControlHandler controlHandler { get; }
        private IAuthHandler _auth;
        private ControlHandlerFactory _controlHandlerFactory;
        public ControlModel(IAuthHandler auth, ControlHandlerFactory controlHandlerFactory)
        {
            _auth = auth;
            UserInfo userInfo = _auth.GetUserInfo();

            _controlHandlerFactory = controlHandlerFactory;
            controlHandler = _controlHandlerFactory.CreateControlHandler(userInfo.RoleType);
        }
    }
}
