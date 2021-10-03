using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace DataAccess.Handlers
{
    public abstract class BaseControlHandler
    {
        private IControlRepository repo;
        public const string securityErrorMessage = "Ошибка доступа (у пользрвателя недостаточно прав)";
        public BaseControlHandler()
        {
            repo = new MsSqlUserRepository();
        }
        public virtual List<UserRecord> GetUserRecords()
        {
            return repo.GetUserRecords();
        }
        public virtual void AddUser(UserRecord rec)
        {
            repo.AddUserRecord(rec);
        }
        public virtual void DeleteUser(UserRecord rec)
        {
            repo.DeleteUserRecord(rec);
        }
    }

    public class UserControlHandler : BaseControlHandler
    {
        public override void AddUser(UserRecord rec)
        {
            throw new SecurityException(securityErrorMessage);
        }
        public override void DeleteUser(UserRecord rec)
        {
            throw new SecurityException(securityErrorMessage);
        }
    }

    public class AdminControlHandler : BaseControlHandler {}
}
