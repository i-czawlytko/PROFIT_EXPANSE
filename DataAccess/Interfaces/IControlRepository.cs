using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IControlRepository
    {
        public List<UserRecord> GetUserRecords();
        public void AddUserRecord(UserRecord rec);
        public void DeleteUserRecord(UserRecord rec);
    }
}
