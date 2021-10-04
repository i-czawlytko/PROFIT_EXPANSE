using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IControlHandler
    {
        void AddUser(UserRecord rec);
        void DeleteUser(UserRecord rec);
        List<UserRecord> GetUserRecords();
    }
}