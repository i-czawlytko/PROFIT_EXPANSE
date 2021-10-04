using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using DataAccess.Interfaces;

namespace DataAccess.Handlers
{
    public class DataHandlerFactory
    {
        IDataRepository _repo;
        IExporter _exporter;
        public DataHandlerFactory(IDataRepository repo, IExporter exporter)
        {
            _repo = repo;
            _exporter = exporter;
        }
        public BaseDataHandler CreateDataHandler(RoleType role)
        {
            switch (role)
            {
                case RoleType.Admin:
                    return new AdminDataHandler(_repo, _exporter);
                case RoleType.User:
                    return new UserDataHandler(_repo, _exporter);
                default:
                    throw new SecurityException("Пользователь отсутствует в базе данных");
            }
        }
    }
}
