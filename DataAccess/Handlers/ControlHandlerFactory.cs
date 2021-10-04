using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using DataAccess.Interfaces;

namespace DataAccess.Handlers
{
    public class ControlHandlerFactory
    {
        IControlRepository _repo;
        public ControlHandlerFactory(IControlRepository repo)
        {
            _repo = repo;
        }
        public BaseControlHandler CreateControlHandler(RoleType role)
        {
            switch (role)
            {
                case RoleType.Admin:
                    return new AdminControlHandler(_repo);
                case RoleType.User:
                    return new UserControlHandler(_repo);
                default:
                    throw new SecurityException();
            }
        }
    }
}
