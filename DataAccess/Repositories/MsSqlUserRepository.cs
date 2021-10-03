using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories
{
    public class MsSqlUserRepository : IControlRepository
    {
        private IConfigurationRoot configuration;
        private IDbConnection db;
        public MsSqlUserRepository()
        {
            db = new SqlConnection(getConnectionString());
        }
        public List<UserRecord> GetUserRecords()
        {
            string sql = "SELECT * from [USER] u JOIN [ROLE] r ON r.[Id] = u.[RoleId];";
            var users = db.Query<User, Role, User>(
                    sql,
                    (user, role) =>
                    {
                        user.Role = role;
                        return user;
                    }).ToList();

            return users.Select(x => new UserRecord
            {
                Id = x.Id,
                Login = x.Login,
                FullName = x.FullName,
                RoleName = x.Role.Name,
                RoleType = (RoleType)x.Role.Id
            }).ToList();
        }
        public void AddUserRecord(UserRecord rec)
        {
            object user = new
            {
                Login = rec.Login,
                FullName = rec.FullName,
                RoleId = (int)rec.RoleType,
                Inserted = DateTime.Now
            };

            var sqlQuery = "INSERT INTO [USER] (Login, FullName, RoleId, Inserted) VALUES(@Login, @FullName, @RoleId, @Inserted)";
            db.Execute(sqlQuery, user);
        }
        public void DeleteUserRecord(UserRecord rec)
        {
            var sqlQuery = "DELETE FROM [USER] WHERE Id = @Id";
            db.Execute(sqlQuery, new { rec.Id });
        }

        private string getConnectionString()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            return configuration.GetConnectionString("Connection");
        }
        
    }
}
